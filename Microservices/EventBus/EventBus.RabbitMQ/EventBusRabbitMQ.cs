using EventBus.Base;
using EventBus.Base.Events;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : BaseEventBus
    {
        RabbitMQPersistentConnection persistenceConnection;
        private readonly IConnectionFactory _connectionFactory;
        private readonly IModel consumerChannel;

        public EventBusRabbitMQ(IServiceProvider serviceProvider, EventBusConfig config) : base(serviceProvider, config)
        {
            if(config.Connection != null)
            {
                var conJson = JsonConvert.SerializeObject(EventBusConfig.Connection, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                _connectionFactory = JsonConvert.DeserializeObject<ConnectionFactory>(conJson);
            }
            else
            {
                _connectionFactory = new ConnectionFactory();
            }


            persistenceConnection = new RabbitMQPersistentConnection(_connectionFactory, config.ConnectionRetryCount);

            consumerChannel = CreateConsumerChannel();

            SubscriptionManager.OnEventRemoved += SubscriptionManager_OnEventRemoved;
        }

        private void SubscriptionManager_OnEventRemoved(object? sender, string eventName)
        {
            eventName = ProcessEventName(eventName);
            if (!persistenceConnection.IsConnected)
            {
                persistenceConnection.TryConnect();
            }

            consumerChannel.QueueUnbind(queue: eventName,
                exchange: EventBusConfig.DefaultTopicName,
                routingKey: eventName);

            if(SubscriptionManager.IsEmpty)
            {
                consumerChannel.Close();
            }
        }

        public override void Publish(IntegrationEvent @event)
        {
            var policy = Policy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(EventBusConfig.ConnectionRetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) => { });
            
            var eventName = @event.GetType().Name; 
            eventName=ProcessEventName(eventName);

            consumerChannel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: "direct");

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {
                var properties = consumerChannel.CreateBasicProperties();
                properties.DeliveryMode = 2;

                //consumerChannel.QueueDeclare(queue: GetSubName(eventName),
                //        durable: true,
                //        exclusive: false,
                //        autoDelete: false,
                //        arguments: null
                //    );

                consumerChannel.BasicPublish(exchange: EventBusConfig.DefaultTopicName,
                        routingKey: eventName,
                        mandatory: true,
                        basicProperties: properties,
                        body: body
                    );
            });
        }

        public override void Subscribe<T, THandler>()
        {
            var eventName = typeof(T).Name;
            eventName = ProcessEventName(eventName);

            if(!SubscriptionManager.HasSubscriptionsForEvent(eventName))
            {
                if(!persistenceConnection.IsConnected)
                {
                    persistenceConnection.TryConnect();
                }
                consumerChannel.QueueDeclare(queue: GetSubName(eventName),
                        durable:true,
                        exclusive:false,
                        autoDelete:false,
                        arguments:null                    
                    );

                consumerChannel.QueueBind(queue: GetSubName(eventName),
                        exchange: EventBusConfig.DefaultTopicName,
                        routingKey: eventName
                    );

            }
            SubscriptionManager.AddSubscription<T, THandler>();

            StartBasicConsume(eventName);
        }

        public override void UnSubscribe<T, THandler>()
        {
            SubscriptionManager.RemoveSubscription<T, THandler>();
        }

        private IModel CreateConsumerChannel()
        {
            if(!persistenceConnection.IsConnected)
            {
                persistenceConnection.TryConnect();
            }

            var channel = persistenceConnection.CreateModel();
            channel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: "direct");

            return channel;
        }

        private void StartBasicConsume(string eventName)
        {
            if(consumerChannel!= null)
            {
                var consumer = new EventingBasicConsumer(consumerChannel);

                consumer.Received += Consumer_Received;

                consumerChannel.BasicConsume(queue:GetSubName(eventName),
                    autoAck:false,
                    consumer:consumer
                    );
            }
        }

        private async void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            eventName = ProcessEventName(eventName);
            var message = Encoding.UTF8.GetString(e.Body.Span);

            try
            {
                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {
                //maybe use logging
            }

            consumerChannel.BasicAck(e.DeliveryTag, multiple: false);
        }
    }
}

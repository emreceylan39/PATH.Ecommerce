using CartService.API.IntegrationEvents.EventHandlers;
using CartService.API.IntegrationEvents.Events;
using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;

namespace CartService.API
{
    public static class EventBusExtensions
    {
        public static void AddRabbitMqOnEventBus(this IServiceCollection services)
        {
            services.AddSingleton<IEventBus>(provider =>
            {
                EventBusConfig config = new EventBusConfig()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    SubscriberClientAppName = "CartService",
                    EventBusType = EventBusType.RabbitMQ
                };

                return EventBusFactory.Create(config, provider);
            });
        }

        public static void SubscribeOrderCreatedEvent(this IServiceProvider services)
        {

            var eventBus = services.GetRequiredService<IEventBus>();
            eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
        }
    }
}

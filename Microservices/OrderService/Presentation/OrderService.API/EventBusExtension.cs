using EventBus.Base.Abstraction;
using EventBus.Base;
using EventBus.Factory;
using OrderService.API.IntegrationEvents.Events;
using OrderService.API.IntegrationEvents.EventHandlers;

namespace OrderService.API
{
    public static class EventBusExtension
    {
        public static void AddRabbitMqOnEventBus(this IServiceCollection services)
        {
            services.AddSingleton<IEventBus>(provider =>
            {
                EventBusConfig config = new EventBusConfig()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    SubscriberClientAppName = "OrderService",
                    EventBusType = EventBusType.RabbitMQ
                };

                return EventBusFactory.Create(config, provider);
            });
        }

        public static void ConfigureEventBusForSubs(this IServiceProvider sp)
        {
            var eventBus = sp.GetRequiredService<IEventBus>();

            eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();

        }
    }
}

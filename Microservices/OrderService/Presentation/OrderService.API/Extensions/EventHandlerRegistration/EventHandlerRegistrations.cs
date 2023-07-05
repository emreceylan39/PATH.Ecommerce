using OrderService.API.IntegrationEvents.EventHandlers;

namespace OrderService.API.Extensions.EventHandlerRegistration
{
    public static class EventHandlerRegistrations
    {
        public static void AddEventHandlerServices (this IServiceCollection services)
        {
            services.AddTransient<OrderCreatedIntegrationEventHandler>();
        }
    }
}

using CartService.API.IntegrationEvents.Events;
using CartService.Application.Abstractions;
using EventBus.Base.Abstraction;

namespace CartService.API.IntegrationEvents.EventHandlers
{
    public class OrderCreatedIntegrationEventHandler :IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {

        private readonly ICartService _cartService;

        public OrderCreatedIntegrationEventHandler(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
            await _cartService.DeleteCartAsync(@event.UserId);
        }
    }
}

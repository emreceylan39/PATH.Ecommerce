using EventBus.Base.Abstraction;
using MediatR;
using OrderService.API.IntegrationEvents.Events;
using OrderService.Application.Features.Commands.CreateOrder;

namespace OrderService.API.IntegrationEvents.EventHandlers
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly IMediator mediator;

        public OrderCreatedIntegrationEventHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
            var createOrderCommand = new CreateOrderCommand(cartItems:@event.Cart.Items,userId:@event.UserId,state:@event.State,city:@event.City,country:@event.Country
                ,cardNumber:@event.CardNumber,cardHolderName:@event.CardHolderName,street:@event.Street);

            await mediator.Send(createOrderCommand);
        }
    }
}

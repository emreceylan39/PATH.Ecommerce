using EventBus.Base.Abstraction;
using MediatR;
using OrderService.Application.IntegrationEvents;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Domain.AggregateModels.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventBus _eventBus;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IEventBus eventBus)
        {
            _orderRepository = orderRepository;
            _eventBus = eventBus;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address = new Address(street: request.Street, city: request.City, state: request.State, country: request.Country);

            Order dbOrder = new Order(userId:request.UserId,quantity:request.OrderItems.Count(),address:address,cardNumber:request.CardNumber,cardHolderName:request.CardHolderName);

            request.OrderItems.ToList().ForEach(i => dbOrder.AddOrderItem(productId: i.ProductId, productName: i.ProductName, price: i.Price, quantity: (int)i.Quantity));
        
            await _orderRepository.AddAsync(dbOrder);
            await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            var orderStartedIntegrationEvent = new OrderStartedIntegrationEvent(request.UserId);

            _eventBus.Publish(orderStartedIntegrationEvent);

            return true;
        }
    }
}

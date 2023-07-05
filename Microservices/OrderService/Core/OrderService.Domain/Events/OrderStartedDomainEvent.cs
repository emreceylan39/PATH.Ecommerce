using MediatR;
using OrderService.Domain.AggregateModels.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Events
{
    public class OrderStartedDomainEvent : INotification
    {

        public string UserId { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }

        public Order Order { get; }

        public OrderStartedDomainEvent(string userId, string cardNumber, string cardHolderName, Order order)
        {
            UserId = userId;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            Order = order;
        }
    }
}

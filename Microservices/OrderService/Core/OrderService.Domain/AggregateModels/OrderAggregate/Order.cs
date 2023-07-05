using OrderService.Domain.Events;
using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class Order: BaseEntity, IAggregateRoot
    {
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
        public Address Address { get; set; }
        private int orderStatusId;
        public OrderStatus OrderStatus { get; set; }
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        protected Order()
        {
            Id= Guid.NewGuid();
            _orderItems = new List<OrderItem>();
        }
        public Order(int quantity, string userId, Address address, string cardNumber,string cardHolderName)
        {
            OrderDate = DateTime.Now;
            orderStatusId = OrderStatus.Submitted.Id;
            Quantity = quantity;
            UserId = userId;
            Address = address;

            AddOrderStartedDomainEvent(userId: userId, cardNumber: cardNumber, cardHolderName: cardHolderName);
        }

        public void AddOrderStartedDomainEvent(string userId, string cardNumber, string cardHolderName)
        {
            var orderStartedDomainEvent = new OrderStartedDomainEvent(userId:userId,cardNumber:cardNumber, cardHolderName:cardHolderName,this);

            this.AddDomainEvent(orderStartedDomainEvent);
        }
       public void AddOrderItem(int productId, string productName, double price, int quantity=1)
        {
            var orderItem = new OrderItem(productId, productName, price, quantity);
            _orderItems.Add(orderItem);
        }


    }
}

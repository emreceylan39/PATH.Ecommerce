using EventBus.Base.Events;
using OrderService.Domain.Models;

namespace OrderService.API.IntegrationEvents.Events
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }
        public int OrderNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public Cart Cart { get; set; }

        public OrderCreatedIntegrationEvent(string userId, int orderNumber, string city, string country, string street, string state, string cardNumber, string cardHolderName, Cart cart)
        {
            UserId = userId;
            OrderNumber = orderNumber;
            City = city;
            Country = country;
            Street = street;
            State = state;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            Cart = cart;
        }
    }
}

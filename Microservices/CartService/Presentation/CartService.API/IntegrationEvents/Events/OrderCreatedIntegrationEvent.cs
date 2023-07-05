using CartService.Domain.Entities;
using EventBus.Base.Events;

namespace CartService.API.IntegrationEvents.Events
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public Cart Cart { get; set; }

        public OrderCreatedIntegrationEvent(string userId, string city, string street, string state, string country, string cardNumber, string cardHolderName, Cart cart)
        {
            UserId = userId;
            City = city;
            Street = street;
            State = state;
            Country = country;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            Cart = cart;
        }
    }
}

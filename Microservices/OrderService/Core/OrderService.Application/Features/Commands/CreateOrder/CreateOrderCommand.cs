using MediatR;
using OrderService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommand :IRequest<bool>
    {
        private readonly List<OrderItemDTO> _orderItems;

        public string UserId { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }

        public IEnumerable<OrderItemDTO> OrderItems => _orderItems;

        public CreateOrderCommand()
        {
            _orderItems = new List<OrderItemDTO>();
        }
        public CreateOrderCommand(List<CartItem> cartItems, string userId, string street, string state, string city, string country, string cardNumber, string cardHolderName) :this()
        {


            var dtoList = cartItems.Select(item => new OrderItemDTO()
            {
                ProductId = item.ProductId,
                ProductName = item.Name,
                Price = item.Price,
                Quantity = item.Quantity

            });

            _orderItems = dtoList.ToList();


            UserId = userId;
            Street = street;
            State = state;
            City = city;
            Country = country;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
        }

        public class OrderItemDTO
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public double Price { get; set; }
            public uint Quantity { get; set; }
        }
    }
}

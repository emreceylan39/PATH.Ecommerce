using CatalogService.Application.Abstractions;
using CatalogService.Domain.DTOs.Product;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastucture.Concretes
{
    public class CartService : ICartService
    {
        public bool AddToCart(ProductDtoForAddToCart productDtoForAddToCart)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri("amqp://guest:guest@localhost:5672/")
                };

                IConnection connection = factory.CreateConnection();
                IModel channel = connection.CreateModel();

                channel.QueueDeclare(queue: "AddCart", exclusive: false);

                string message = JsonConvert.SerializeObject(productDtoForAddToCart);
                channel.BasicPublish(exchange: "", routingKey: "AddCart", body: Encoding.UTF8.GetBytes(message));

                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool ClearCart(int userId)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri("amqp://guest:guest@localhost:5672/")
                };

                IConnection connection = factory.CreateConnection();
                IModel channel = connection.CreateModel();

                channel.QueueDeclare(queue: "ClearCart", exclusive: false);

                channel.BasicPublish(exchange: "", routingKey: "ClearCart", body: Encoding.UTF8.GetBytes(userId.ToString()));

                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool RemoveFromCart(ProductDtoForRemoveFromCart productDtoForRemoveFromCart)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri("amqp://guest:guest@localhost:5672/")
                };

                IConnection connection = factory.CreateConnection();
                IModel channel = connection.CreateModel();

                channel.QueueDeclare(queue: "RemoveCart", exclusive: false);

                string message = JsonConvert.SerializeObject(productDtoForRemoveFromCart);
                channel.BasicPublish(exchange: "", routingKey: "RemoveCart", body: Encoding.UTF8.GetBytes(message));

                return true;

            }
            catch
            {
                return false;
            }
        }
    }
}

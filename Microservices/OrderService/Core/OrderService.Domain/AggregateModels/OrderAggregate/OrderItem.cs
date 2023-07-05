using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class OrderItem : BaseEntity, IValidatableObject
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        protected OrderItem()
        {
            
        }
        public OrderItem(int productId, string productName, double price, int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var result = new List<ValidationResult>();
            if(Quantity<=0)
            {
                result.Add(new ValidationResult("Invalid quantity", new[] { "Quantity" }));

            }

            return result;
        }
    }
}

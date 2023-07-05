using OrderService.Domain.Exceptions;
using OrderService.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.AggregateModels.OrderAggregate
{
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Submitted = new(1, nameof(Submitted).ToLowerInvariant());
        public static OrderStatus AwaitingValidation = new(2, nameof(AwaitingValidation).ToLowerInvariant());
        public static OrderStatus StockConfirmed = new(3, nameof(Submitted).ToLowerInvariant());



        public OrderStatus(int id, string name) : base(id, name)
        {

        }
        public static IEnumerable<OrderStatus> List() =>
            new[] { Submitted, AwaitingValidation, StockConfirmed };
        public static OrderStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(x=>String.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
            return state ?? throw new OrderingDomainException($"Possible values for Order Status:{String.Join(",",List().Select(x=>x.Name))}");

        } 
        public static OrderStatus From(int id)
        {
            var state = List().SingleOrDefault(x => x.Id == id);
            return state ?? throw new OrderingDomainException($"Possible values for Order Status:{String.Join(",", List().Select(x => x.Name))}");
        }
    }
}

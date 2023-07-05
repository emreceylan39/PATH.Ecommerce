using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public uint Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Models
{
    public class Cart
    {
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; }

        public Cart(string userId)
        {
            UserId = userId;
            Items = new List<CartItem>();
        }
    }
}

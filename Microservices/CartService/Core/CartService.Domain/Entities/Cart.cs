using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Domain.Entities
{
    public class Cart
    {

        public string UserId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public Cart()
        {
            
        }
        public Cart(string userId)
        {
            UserId = userId;
        }
    }
}

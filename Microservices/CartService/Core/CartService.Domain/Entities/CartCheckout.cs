using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Domain.Entities
{
    public class CartCheckout
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string UserId { get; set; }


    }
}

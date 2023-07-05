using CartService.Application.Abstractions;

using CartService.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CartService.Persistence.Concretes
{
    public class CartServices : ICartService
    {
        private readonly IConnectionMultiplexer _redis;
 
        public CartServices(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<bool> DeleteCartAsync(string userId)
        {
            
            var db = _redis.GetDatabase();
            return await db.KeyDeleteAsync(userId);
        }

        public async Task<Cart> GetCartAsync(string userId)
        {
            var db = _redis.GetDatabase();
            var data = await db.StringGetAsync(userId);
            if (data.IsNullOrEmpty)
                return null;

            return JsonConvert.DeserializeObject<Cart>(data);
        }

      

        public async Task<Cart> UpdateCartAsync(Cart cart)
        {
            var db = _redis.GetDatabase();
            var created = await db.StringSetAsync(cart.UserId.ToString(),JsonConvert.SerializeObject(cart),expiry: new TimeSpan(0,30,0));

            if (!created)
                return null;

            return await GetCartAsync(cart.UserId.ToString());

        }
    }
}

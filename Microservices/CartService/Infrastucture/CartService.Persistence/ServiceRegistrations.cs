using CartService.Application.Abstractions;
using CartService.Persistence.Concretes;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;


namespace CartService.Persistence
{
    public static class ServiceRegistrations
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            var multiplexer = ConnectionMultiplexer.Connect("localhost:6379");
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            services.AddTransient<ICartService, CartServices>();
            services.AddStackExchangeRedisCache(options=> options.Configuration = "localhost:6379");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Infrastucture.Context;
using OrderService.Infrastucture.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastucture
{
    public static class ServiceRegistration
    {
        public static void AddInfrastuctureServices(this IServiceCollection services)
        {
            services.AddDbContext<OrderDbContext>(opt =>
            {
                opt.UseSqlServer("Data Source=localhost; Initial Catalog=OrderSevice; Persist Security=true, User ID=sa; Password=master.12");
            });

            services.AddScoped<IOrderRepository, OrderRepository>();

            var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>().UseSqlServer("Data Source=localhost; Initial Catalog=OrderSevice; Persist Security=true, User ID=sa; Password=master.12");

            using var dbContext = new OrderDbContext(optionsBuilder.Options, null);

            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();

        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Features.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application
{
    public static class ServiceRegistrations
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}

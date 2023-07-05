using CatalogService.Application.Abstractions;
using CatalogService.Infrastucture.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Infrastucture
{
    public static class ServiceRegistration
    {
        public static void AddInfrastuctureServices(this IServiceCollection services)
        {
            services.AddScoped<ICartService,CartService>();
        }
    }
}

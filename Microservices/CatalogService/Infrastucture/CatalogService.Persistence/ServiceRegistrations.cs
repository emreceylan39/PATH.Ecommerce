using CatalogService.Application.Abstractions;
using CatalogService.Domain.DbContexts;
using CatalogService.Persistence.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Persistence
{
    public static class ServiceRegistrations
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<CatalogServiceDbContext>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}

using LoginService.Application.Abstractions;
using LoginService.Domain.Contexts;
using LoginService.Persistence.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LoginService.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);


            services.AddDbContext<LoginDbContext>();
            services.AddScoped<IAuthService,AuthService>();



        }
    }
}

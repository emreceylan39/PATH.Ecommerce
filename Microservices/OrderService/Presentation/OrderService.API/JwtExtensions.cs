using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OrderService.API
{
    public static class JwtExtensions
    {
        public const string SecurityKey = "aG9yc2V0aWxsbmF0aW9udGlnaHRseXNwZWNpYWxwaWNrZmlyZXBsYWNlc2hvcnRudXQ=";
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = "https://localhost:2000",
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey))
                };
            });
        }
    }
}

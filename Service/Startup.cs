using System;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Service.Data;
using Service.Data.Models;

namespace Service
{
    public static class StartupExtension
    {
        public static void UseServiceContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ServiceContext>(options =>
                options.UseSqlServer(connectionString));

        }

        public static void UseServiceAuthentication(this IServiceCollection services, string jwtValidAudience, string jwtValidIssuer, string jwtSecret)
        {
            services.AddScoped<Service.Services.AuthenticationService>();

            // For Identity  
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ServiceContext>()
                .AddDefaultTokenProviders();

            // Adding Authentication  
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = jwtValidAudience,
                        ValidIssuer = jwtValidIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
                    };
                });
        }

        public static void MigrateServiceContext(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ServiceContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger>();
                    logger.LogError(ex, "An error occurred migrating the ServiceContext Database.");
                }
            }
        }

    }
}
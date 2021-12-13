using System;
using Core.Extensions.IHostEnvironmentExtension;
using Core.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Service;

namespace API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostEnvironment)
        {
            Configuration = hostEnvironment.BuildConfiguration();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.UseServiceContext(Configuration.GetConnectionString("ServiceContextConnection"));
            services.UseServiceAuthentication(Configuration["JWT:ValidAudience"], Configuration["JWT:ValidIssuer"], Configuration["JWT:Secret"]);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}");

                c.AddServiceSecurity();
            });

            services.AddCors(o => o.AddPolicy("Development", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopmentBase())
            {
                app.MigrateServiceContext(serviceProvider);

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

                app.UseCors("Development");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

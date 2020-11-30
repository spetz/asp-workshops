using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Trill.Application;
using Trill.Application.Services;
using Trill.Infrastructure;

namespace Trill.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddControllers();
            services.AddApplication();
            services.AddInfrastructure();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseInfrastructure();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
                endpoints.MapGet("api", async context =>
                {
                    var serviceId1 = context.RequestServices.GetRequiredService<IServiceId>();
                    var serviceId2 = context.RequestServices.GetRequiredService<IServiceId>();
                    await context.Response.WriteAsync("Trill API");
                });

                endpoints.MapGet("api/stories/{storyId:guid}", async context =>
                {
                    var storyId = Guid.Parse(context.Request.RouteValues["storyId"].ToString());
                    if (storyId == Guid.Empty)
                    {
                        context.Response.StatusCode = 404;
                        return;
                    }
                    
                    // Application service -> get story from DB()
                    // Serialize to JSON

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{}");
                });
                
                endpoints.MapPost("api/stories", async context =>
                {
                    var storyId = Guid.NewGuid();
                    context.Response.Headers.Add("Location", $"api/stories/{storyId}");
                    context.Response.StatusCode = 201;
                });
            });
        }
    }
}

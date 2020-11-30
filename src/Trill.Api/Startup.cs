using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Trill.Application;
using Trill.Application.Requests;
using Trill.Application.Services;
using Trill.Infrastructure;

namespace Trill.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddControllers().AddNewtonsoftJson();
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
                    var serviceId = context.RequestServices.GetRequiredService<IServiceId>().GetId();
                    await context.Response.WriteAsync("Trill API");
                });

                endpoints.MapGet("api/stories/{storyId:guid}", async context =>
                {
                    var storyId = Guid.Parse(context.Request.RouteValues["storyId"].ToString());
                    var storyService = context.RequestServices.GetRequiredService<IStoryService>();
                    var story = await storyService.GetAsync(storyId);
                    if (story is null)
                    {
                        context.Response.StatusCode = 404;
                        return;
                    }

                    var json = JsonConvert.SerializeObject(story);
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(json);
                });
                
                endpoints.MapPost("api/stories", async context =>
                {
                    var json = await new StreamReader(context.Request.Body).ReadToEndAsync();
                    var payload = JsonConvert.DeserializeObject<SendStory>(json);
                    var storyService = context.RequestServices.GetRequiredService<IStoryService>();
                    await storyService.AddAsync(payload);
                    context.Response.Headers.Add("Location", $"api/stories/{payload.Id}");
                    context.Response.StatusCode = 201;
                });
            });
        }
    }
}

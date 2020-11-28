using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Trill.Application;
using Trill.Infrastructure;

namespace Trill.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
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
            });
        }
    }
}

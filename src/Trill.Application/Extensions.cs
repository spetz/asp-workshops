using Microsoft.Extensions.DependencyInjection;
using Trill.Application.Services;
using Trill.Core.Factories;
using Trill.Core.Services;

namespace Trill.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IStoryService, StoryService>();
            services.AddSingleton<IRefreshTokenFactory, RefreshTokenFactory>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            
            return services;
        }
    }
}
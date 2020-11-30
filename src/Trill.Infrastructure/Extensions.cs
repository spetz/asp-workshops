using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trill.Application.Services;
using Trill.Core.Services;
using Trill.Infrastructure.Auth;
using Trill.Infrastructure.Exceptions;
using Trill.Infrastructure.Logging;
using Trill.Infrastructure.Mongo;
using Trill.Infrastructure.Services;

namespace Trill.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                services.Configure<ApiOptions>(configuration.GetSection("api"));
            }

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IServiceId, ServiceId>();
            // services.AddHostedService<NotificationJob>();
            
            services.AddMemoryCache();
            services.AddSingleton<IRng, Rng>();
            services.AddMongo();
            services.AddAuth();
            services.Decorate<IStoryService, StoryServiceCacheDecorator>();
            services.AddSingleton<LoggingMiddleware>();
            services.AddSingleton<ErrorHandlerMiddleware>();
            services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();
            
            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseMiddleware<LoggingMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            
            return app;
        }
        
        public static long ToUnixTimeMilliseconds(this DateTime dateTime)
            => new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();

        public static string Underscore(this string value)
            => string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString()))
                    .ToLowerInvariant();
        
        public static string GetExceptionCode(this Exception exception)
            => exception.GetType().Name.Underscore().Replace("_exception", string.Empty);
    }
}
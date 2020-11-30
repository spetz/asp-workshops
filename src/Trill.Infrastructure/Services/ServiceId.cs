using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Trill.Application.Services;

namespace Trill.Infrastructure.Services
{
    internal class ServiceId : IServiceId
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ServiceId> _logger;
        private readonly string _id = Guid.NewGuid().ToString("N");

        public ServiceId(IHttpContextAccessor httpContextAccessor, ILogger<ServiceId> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public string GetId()
        {
            _logger.LogInformation("Getting an ID...");
            var context = _httpContextAccessor.HttpContext;
            return _id;
        }
    }
}
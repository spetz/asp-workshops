using System;
using Microsoft.AspNetCore.Http;
using Trill.Application.Services;

namespace Trill.Infrastructure.Services
{
    internal class ServiceId : IServiceId
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _id = Guid.NewGuid().ToString("N");

        public ServiceId(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetId()
        {
            var context = _httpContextAccessor.HttpContext;
            return _id;
        }
    }
}
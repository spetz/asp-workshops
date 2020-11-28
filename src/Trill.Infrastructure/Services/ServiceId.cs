using System;
using Trill.Application.Services;

namespace Trill.Infrastructure.Services
{
    internal class ServiceId : IServiceId
    {
        private readonly string _id = Guid.NewGuid().ToString("N");

        public string GetId() => _id;
    }
}
using System;
using Trill.Core.Services;

namespace Trill.Application.Services
{
    internal class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now  => DateTime.UtcNow;
    }
}
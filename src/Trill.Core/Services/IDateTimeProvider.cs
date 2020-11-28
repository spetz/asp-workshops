using System;

namespace Trill.Core.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
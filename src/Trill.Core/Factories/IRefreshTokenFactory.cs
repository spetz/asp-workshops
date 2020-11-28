using System;
using Trill.Core.Entities;

namespace Trill.Core.Factories
{
    public interface IRefreshTokenFactory
    {
        RefreshToken Create(Guid userId);
    }
}
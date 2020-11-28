using System;
using System.Collections.Generic;
using Trill.Application.DTO;

namespace Trill.Application.Services
{
    public interface IJwtProvider
    {
        AuthDto Create(Guid userId, string username, string role, string audience = null,
            IDictionary<string, IEnumerable<string>> claims = null);
    }
}
using Trill.Core.Exceptions;

namespace Trill.Application.Exceptions
{
    internal class RevokedRefreshTokenException : DomainException
    {
        public RevokedRefreshTokenException() : base("Revoked refresh token.")
        {
        }
    }
}
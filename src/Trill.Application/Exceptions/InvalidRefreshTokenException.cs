using Trill.Core.Exceptions;

namespace Trill.Application.Exceptions
{
    internal class InvalidRefreshTokenException : DomainException
    {
        public InvalidRefreshTokenException() : base("Invalid refresh token.")
        {
        }
    }
}
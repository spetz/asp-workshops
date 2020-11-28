namespace Trill.Core.Exceptions
{
    internal class InvalidRefreshTokenException : DomainException
    {
        public InvalidRefreshTokenException() : base("Invalid refresh token.")
        {
        }
    }
}
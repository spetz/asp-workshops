namespace Trill.Core.Exceptions
{
    internal class EmptyRefreshTokenException : DomainException
    {
        public EmptyRefreshTokenException() : base("Empty refresh token.")
        {
        }
    }
}
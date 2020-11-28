namespace Trill.Core.Exceptions
{
    public class InvalidAuthorException : DomainException
    {
        public InvalidAuthorException(string author) : base($"Invalid author: '{author}'.")
        {
        }
    }
}
namespace Trill.Core.Exceptions
{
    internal class InvalidPasswordException : DomainException
    {
        public InvalidPasswordException() : base("Invalid password.")
        {
        }
    }
}
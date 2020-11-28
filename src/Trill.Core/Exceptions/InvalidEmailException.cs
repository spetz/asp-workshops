namespace Trill.Core.Exceptions
{
    internal class InvalidEmailException : DomainException
    {
        public InvalidEmailException(string email) : base($"Invalid email: {email}.")
        {
        }
    }
}
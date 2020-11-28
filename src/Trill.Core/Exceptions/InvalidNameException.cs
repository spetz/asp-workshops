namespace Trill.Core.Exceptions
{
    internal class InvalidNameException : DomainException
    {
        public InvalidNameException(string name) : base($"Invalid name: {name}.")
        {
        }
    }
}
using Trill.Core.Exceptions;

namespace Trill.Application.Exceptions
{
    internal class InvalidCredentialsException : DomainException
    {
        public string Name { get; }

        public InvalidCredentialsException(string name) : base("Invalid credentials.")
        {
            Name = name;
        }
    }
}
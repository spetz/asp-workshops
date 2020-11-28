namespace Trill.Core.Exceptions
{
    internal class InvalidCredentialsException : DomainException
    {
        public string Email { get; }

        public InvalidCredentialsException(string email) : base("Invalid credentials.")
        {
            Email = email;
        }
    }
}
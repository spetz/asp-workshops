namespace Trill.Core.Exceptions
{
    internal class InvalidRoleException : DomainException
    {
        public InvalidRoleException(string role) : base($"Invalid role: {role}.")
        {
        }
    }
}
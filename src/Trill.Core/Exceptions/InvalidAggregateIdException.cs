namespace Trill.Core.Exceptions
{
    internal class InvalidAggregateIdException : DomainException
    {
        public InvalidAggregateIdException() : base("Invalid aggregate id.")
        {
        }
    }
}
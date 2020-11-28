namespace Trill.Core.Exceptions
{
    public class MissingTextException : DomainException
    {
        public MissingTextException() : base("Missing text.")
        {
        }
    }
}
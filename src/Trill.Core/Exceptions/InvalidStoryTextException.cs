namespace Trill.Core.Exceptions
{
    internal class InvalidStoryTextException : DomainException
    {
        public InvalidStoryTextException() : base("Invalid story text.")
        {
        }
    }
}
namespace Trill.Core.Exceptions
{
    public class MissingTitleException : DomainException
    {
        public MissingTitleException() : base("Missing title.")
        {
        }
    }
}
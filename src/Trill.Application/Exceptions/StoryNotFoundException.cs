namespace Trill.Application.Exceptions
{
    internal class StoryNotFoundException : AppException
    {
        public StoryNotFoundException(long storyId) : base($"Story with ID: '{storyId}' was not found.")
        {
        }
    }
}
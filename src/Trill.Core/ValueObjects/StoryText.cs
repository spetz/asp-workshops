using System;
using Trill.Core.Exceptions;

namespace Trill.Core.ValueObjects
{
    public class StoryText : IEquatable<StoryText>
    {
        public string Value { get; private set; }

        private StoryText()
        {
        }
        
        public StoryText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new InvalidStoryTextException();
            }
            
            Value = text.Trim();
        }

        public static implicit operator string(StoryText storyText) => storyText.Value;

        public static implicit operator StoryText(string storyText) => new StoryText(storyText);

        public bool Equals(StoryText other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((StoryText) obj);
        }

        public override int GetHashCode() => Value != null ? Value.GetHashCode() : 0;
    }
}
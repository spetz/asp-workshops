using System;
using System.Collections.Generic;
using System.Linq;
using Trill.Core.Exceptions;
using Trill.Core.ValueObjects;

namespace Trill.Core.Entities
{
    public class Story
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public StoryText Text { get; private set; }
        public Author Author { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Story()
        {
        }

        public Story(Guid id, string title, StoryText text, Author author, IEnumerable<string> tags, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new MissingTitleException();
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new MissingTextException();
            }

            Id = id;
            Title = title.Trim();
            Text = text;
            Author = author;
            Tags = tags ?? Enumerable.Empty<string>();
            CreatedAt = createdAt;
        }
    }
}
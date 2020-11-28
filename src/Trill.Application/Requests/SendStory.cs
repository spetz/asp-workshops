using System;
using System.Collections.Generic;

namespace Trill.Application.Requests
{
    public class SendStory
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Text { get; }
        public Guid UserId { get; set; }
        public IEnumerable<string> Tags { get; }

        public SendStory(Guid id, string title, string text, Guid userId, IEnumerable<string> tags = null)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Title = title;
            Text = text;
            UserId = userId;
            Tags = tags;
        }
    }
}
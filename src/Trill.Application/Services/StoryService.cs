using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Trill.Application.DTO;
using Trill.Application.Exceptions;
using Trill.Application.Requests;
using Trill.Core.Entities;
using Trill.Core.Repositories;
using Trill.Core.Services;
using Trill.Core.ValueObjects;

namespace Trill.Application.Services
{
    public class StoryService : IStoryService
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger<StoryService> _logger;

        public StoryService(IStoryRepository storyRepository, IUserRepository userRepository,
            IDateTimeProvider dateTimeProvider, ILogger<StoryService> logger)
        {
            _storyRepository = storyRepository;
            _userRepository = userRepository;
            _dateTimeProvider = dateTimeProvider;
            _logger = logger;
        }

        public async Task<StoryDetailsDto> GetAsync(Guid id)
        {
            var story = await _storyRepository.GetAsync(id);

            return story is null
                ? null
                : new StoryDetailsDto
                {
                    Id = story.Id,
                    Author = story.Author,
                    Text = story.Text,
                    Title = story.Title,
                    Tags = story.Tags ?? Enumerable.Empty<string>(),
                    CreatedAt = story.CreatedAt
                };
        }

        public async Task<IEnumerable<StoryDto>> BrowseAsync(BrowseStories request)
        {
            Expression<Func<Story, bool>> query = story =>
                (request.Author == null || story.Author.Name == request.Author) &&
                (request.Title == null || story.Title.Contains(request.Title ?? string.Empty)) &&
                (request.Text == null || story.Text.Value.Contains(request.Text ?? string.Empty));
            
            var stories = await _storyRepository.BrowseAsync(query);

            return stories.Select(x => new StoryDto
            {
                Id = x.Id,
                Author = x.Author,
                Title = x.Title,
                Tags = x.Tags ?? Enumerable.Empty<string>(),
                CreatedAt = x.CreatedAt
            });
        }

        public async Task AddAsync(SendStory request)
        {
            var user = await _userRepository.GetAsync(request.UserId);
            if (user is null)
            {
                throw new UserNotFoundException(request.UserId);
            }

            if (user.Locked)
            {
                throw new UserLockedException(user.Id);
            }

            var author = new Author(user.Name);
            var story = new Story(request.Id, request.Title, request.Text, author, request.Tags, _dateTimeProvider.Now);
            await _storyRepository.AddAsync(story);
            _logger.LogInformation($"Added a story with ID: '{story.Id}'.");
        }
    }
}
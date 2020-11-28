using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trill.Application.DTO;
using Trill.Application.Requests;

namespace Trill.Application.Services
{
    public interface IStoryService
    {
        Task<StoryDetailsDto> GetAsync(Guid id);
        Task<IEnumerable<StoryDto>> BrowseAsync(BrowseStories request);
        Task AddAsync(SendStory request);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trill.Application.DTO;
using Trill.Application.Requests;
using Trill.Application.Services;

namespace Trill.Api.Controllers
{
    public class StoriesController : BaseController
    {
        private readonly IStoryService _storyService;

        public StoriesController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoryDto>>> Get([FromQuery] BrowseStories request)
            => Ok(await _storyService.BrowseAsync(request));

        [HttpGet("{storyId:guid}")]
        public async Task<ActionResult<StoryDetailsDto>> Get(Guid storyId)
        {
            var story = await _storyService.GetAsync(storyId);
            if (story is null)
            {
                return NotFound();
            }

            return Ok(story);
        }

        [HttpPost]
        public async Task<ActionResult> Post(SendStory request)
        {
            await _storyService.AddAsync(request);
            return CreatedAtAction(nameof(Get), new {storyId = request.Id}, null);
        }
    }
}
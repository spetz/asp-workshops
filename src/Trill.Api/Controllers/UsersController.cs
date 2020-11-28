using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trill.Application.DTO;
using Trill.Application.Services;

namespace Trill.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IIdentityService _identityService;

        public UsersController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        
        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<UserDetailsDto>> Get(Guid userId)
        {
            var user = await _identityService.GetUserAsync(userId);
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
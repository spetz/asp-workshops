using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trill.Application.DTO;
using Trill.Application.Requests;
using Trill.Application.Services;

namespace Trill.Api.Controllers
{
    [Route("")]
    public class AccountController : BaseController
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp(SignUp request)
        {
            await _identityService.SignUpAsync(request);
            return Created("me", null);
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult<AuthDto>> SignIn(SignIn request)
            => Ok(await _identityService.SignInAsync(request));

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserDetailsDto>> Me()
            => Ok(await _identityService.GetUserAsync(UserId));
    }
}
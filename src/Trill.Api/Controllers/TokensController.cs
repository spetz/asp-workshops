using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trill.Application.DTO;
using Trill.Application.Requests;
using Trill.Application.Services;

namespace Trill.Api.Controllers
{
    public class TokensController : BaseController
    {
        private readonly IRefreshTokenService _refreshTokenService;

        public TokensController(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("use")]
        public async Task<ActionResult<AuthDto>> Use(UseRefreshToken request)
            => await _refreshTokenService.UseAsync(request.RefreshToken);

        [HttpPost("revoke")]
        public async Task<ActionResult<AuthDto>> Revoke(RevokeRefreshToken request)
        {
            await _refreshTokenService.RevokeAsync(request.RefreshToken);
            return NoContent();
        }
    }
}
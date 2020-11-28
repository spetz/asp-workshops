using System.Threading.Tasks;
using Trill.Application.DTO;

namespace Trill.Application.Services
{
    public interface IRefreshTokenService
    {
        Task<AuthDto> UseAsync(string refreshToken);
        Task RevokeAsync(string refreshToken);
    }
}
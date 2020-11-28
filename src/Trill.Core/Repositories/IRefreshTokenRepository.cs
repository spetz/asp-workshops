using System.Threading.Tasks;
using Trill.Core.Entities;

namespace Trill.Core.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);
        Task AddAsync(RefreshToken refreshToken);
        Task UpdateAsync(RefreshToken refreshToken);
    }
}
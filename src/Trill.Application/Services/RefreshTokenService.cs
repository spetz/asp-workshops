using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trill.Application.DTO;
using Trill.Application.Exceptions;
using Trill.Core.Factories;
using Trill.Core.Repositories;
using Trill.Core.Services;

namespace Trill.Application.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenFactory _refreshTokenFactory;
        private readonly IJwtProvider _jwtProvider;
        private readonly IDateTimeProvider _dateTimeProvider;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository,
            IRefreshTokenFactory refreshTokenFactory, IJwtProvider jwtProvider, IDateTimeProvider dateTimeProvider)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _refreshTokenFactory = refreshTokenFactory;
            _jwtProvider = jwtProvider;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<AuthDto> UseAsync(string refreshToken)
        {
            var token = await _refreshTokenRepository.GetAsync(refreshToken);
            if (token is null)
            {
                throw new InvalidRefreshTokenException();
            }

            token.Use(_dateTimeProvider.Now);
            var user = await _userRepository.GetAsync(token.UserId);
            if (user is null)
            {
                throw new UserNotFoundException(token.UserId);
            }

            var claims = user.Permissions.Any()
                ? new Dictionary<string, IEnumerable<string>>
                {
                    ["permissions"] = user.Permissions
                }
                : null;

            var auth = _jwtProvider.Create(token.UserId, user.Name, user.Role, claims: claims);
            var newRefreshToken = _refreshTokenFactory.Create(user.Id);
            await _refreshTokenRepository.AddAsync(newRefreshToken);
            await _refreshTokenRepository.UpdateAsync(token);
            auth.RefreshToken = newRefreshToken.Token;

            return auth;
        }

        public async Task RevokeAsync(string refreshToken)
        {
            var token = await _refreshTokenRepository.GetAsync(refreshToken);
            if (token is null)
            {
                throw new InvalidRefreshTokenException();
            }
            
            token.Revoke(_dateTimeProvider.Now);
            await _refreshTokenRepository.UpdateAsync(token);
        }
    }
}
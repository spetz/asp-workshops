using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Trill.Application.DTO;
using Trill.Application.Exceptions;
using Trill.Application.Requests;
using Trill.Core.Entities;
using Trill.Core.Factories;
using Trill.Core.Repositories;
using Trill.Core.Services;

namespace Trill.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtProvider _jwtProvider;
        private readonly IRefreshTokenFactory _refreshTokenFactory;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository,
            IPasswordService passwordService, IJwtProvider jwtProvider, IRefreshTokenFactory refreshTokenFactory,
            IDateTimeProvider dateTimeProvider, ILogger<IdentityService> logger)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _passwordService = passwordService;
            _jwtProvider = jwtProvider;
            _refreshTokenFactory = refreshTokenFactory;
            _dateTimeProvider = dateTimeProvider;
            _logger = logger;
        }

        public async Task<UserDetailsDto> GetUserAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);

            return user is null
                ? null
                : new UserDetailsDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Role = user.Role,
                    Permissions = user.Permissions ?? Enumerable.Empty<string>(),
                    Locked = user.Locked,
                    CreatedAt = user.CreatedAt
                };
        }

        public async Task SignUpAsync(SignUp request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user is {})
            {
                _logger.LogError($"Email is already in use: '{request.Email}'.");
                throw new EmailInUseException(request.Email);
            }

            user = await _userRepository.GetByNameAsync(request.Name);
            if (user is {})
            {
                _logger.LogError($"Name is already in use: '{request.Name}'.");
                throw new NameInUseException(request.Name);
            }

            var password = _passwordService.Hash(request.Password);
            user = new User(request.Id, request.Email, request.Name, password, request.Role, _dateTimeProvider.Now,
                request.Permissions);
            await _userRepository.AddAsync(user);
            _logger.LogInformation($"User with ID: '{user.Id:N}' has been registered.");
        }

        public async Task<AuthDto> SignInAsync(SignIn request)
        {
            var user = await _userRepository.GetByNameAsync(request.Name);
            if (user is null || !_passwordService.IsValid(user.Password, request.Password))
            {
                _logger.LogError($"User with name: {request.Name} was not found.");
                throw new InvalidCredentialsException(request.Name);
            }

            if (user.Locked)
            {
                throw new UserLockedException(user.Id);
            }

            var claims = user.Permissions.Any()
                ? new Dictionary<string, IEnumerable<string>>
                {
                    ["permissions"] = user.Permissions
                }
                : null;
            var auth = _jwtProvider.Create(user.Id, user.Name, user.Role, claims: claims);
            auth.RefreshToken = await CreateRefreshTokenAsync(user.Id);
            _logger.LogInformation($"User with ID: '{user.Id:N}' has been authenticated.");

            return auth;
        }

        private async Task<string> CreateRefreshTokenAsync(Guid userId)
        {
            var refreshToken = _refreshTokenFactory.Create(userId);
            await _refreshTokenRepository.AddAsync(refreshToken);

            return refreshToken.Token;
        }
    }
}
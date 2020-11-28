using System;
using System.Threading.Tasks;
using Trill.Application.DTO;
using Trill.Application.Requests;

namespace Trill.Application.Services
{
    public interface IIdentityService
    {
        Task<UserDetailsDto> GetUserAsync(Guid id);
        Task SignUpAsync(SignUp request);
        Task<AuthDto> SignInAsync(SignIn request);
    }
}
using System.Collections.Generic;

namespace Trill.Application.DTO
{
    public class UserDetailsDto : UserDto
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trill.Application.Requests
{
    public class SignUp
    {
        public Guid Id { get; }
        public string Email { get; }
        public string Name { get; }
        public string Password { get; }
        public string Role { get; }
        public IEnumerable<string> Permissions { get; }

        public SignUp(Guid userId, string email, string name, string password, string role = "user",
            IEnumerable<string> permissions = null)
        {
            Id = userId == Guid.Empty ? Guid.NewGuid() : userId;
            Email = email;
            Name = name;
            Password = password;
            Role = role;
            Permissions = permissions ?? Enumerable.Empty<string>();
        }
    }
}
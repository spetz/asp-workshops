using System;
using System.Collections.Generic;
using System.Linq;
using Trill.Core.Exceptions;

namespace Trill.Core.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public string Role { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public IEnumerable<string> Permissions { get; private set; }
        public bool Locked { get; private set; }
        
        private User()
        {
        }

        public User(Guid id, string email, string name, string password, string role, DateTime createdAt,
            IEnumerable<string> permissions = null, bool locked = false)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidEmailException(email);
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidNameException(name);
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidPasswordException();
            }

            if (!Entities.Role.IsValid(role))
            {
                throw new InvalidRoleException(role);
            }

            Id = id;
            Email = email.ToLowerInvariant();
            Name = name.Trim();
            Password = password;
            Role = role.ToLowerInvariant();
            CreatedAt = createdAt;
            Permissions = permissions ?? Enumerable.Empty<string>();
            Locked = locked;
        }

        public bool Lock()
        {
            if (Locked)
            {
                return false;
            }

            Locked = true;
            return true;
        }

        public bool Unlock()
        {
            if (!Locked)
            {
                return false;
            }

            Locked = false;
            return true;
        }
    }
}
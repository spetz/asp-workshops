using System;
using Trill.Core.Exceptions;

namespace Trill.Core.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public bool Revoked => RevokedAt.HasValue;

        public RefreshToken(Guid id, Guid userId, string token, DateTime createdAt, DateTime? revokedAt = null)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new EmptyRefreshTokenException();
            }

            Id = id;
            UserId = userId;
            Token = token;
            CreatedAt = createdAt;
            RevokedAt = revokedAt;
        }

        public void Use(DateTime dateTime)
        {
            if (Revoked)
            {
                throw new RevokedRefreshTokenException();
            }

            RevokedAt = dateTime;
        }

        public void Revoke(DateTime revokedAt)
        {
            if (Revoked)
            {
                throw new RevokedRefreshTokenException();
            }

            RevokedAt = revokedAt;
        }
    }
}
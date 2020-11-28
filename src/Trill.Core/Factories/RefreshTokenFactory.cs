using System;
using Trill.Core.Entities;
using Trill.Core.Services;

namespace Trill.Core.Factories
{
    public class RefreshTokenFactory : IRefreshTokenFactory
    {
        private readonly IRng _rng;
        private readonly IDateTimeProvider _dateTimeProvider;

        public RefreshTokenFactory(IRng rng, IDateTimeProvider dateTimeProvider)
        {
            _rng = rng;
            _dateTimeProvider = dateTimeProvider;
        }

        public RefreshToken Create(Guid userId)
            => new RefreshToken(Guid.NewGuid(), userId, _rng.Generate(), _dateTimeProvider.Now);
    }
}
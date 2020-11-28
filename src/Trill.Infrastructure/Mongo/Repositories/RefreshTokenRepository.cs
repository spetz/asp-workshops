using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Trill.Core.Entities;
using Trill.Core.Repositories;

namespace Trill.Infrastructure.Mongo.Repositories
{
    internal class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoCollection<RefreshToken> _collection;

        public RefreshTokenRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<RefreshToken>("refreshTokens");
        }

        public async Task<RefreshToken> GetAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return default;
            }

            return await _collection.AsQueryable().SingleOrDefaultAsync(x => x.Token == token);
        }

        public Task AddAsync(RefreshToken refreshToken)
            => _collection.InsertOneAsync(refreshToken);

        public Task UpdateAsync(RefreshToken refreshToken)
            => _collection.ReplaceOneAsync(x => x.Id == refreshToken.Id, refreshToken);
    }
}
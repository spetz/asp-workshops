using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Trill.Core.Entities;
using Trill.Core.Repositories;

namespace Trill.Infrastructure.Mongo.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<User>("users");
        }

        public Task<User> GetAsync(Guid id)
            => _collection.AsQueryable().SingleOrDefaultAsync(x => x.Id == id);

        public Task<User> GetByEmailAsync(string email)
            => string.IsNullOrWhiteSpace(email)
                ? Task.FromResult<User>(default)
                : _collection.AsQueryable().SingleOrDefaultAsync(x => x.Email == email.ToLowerInvariant());

        public Task<User> GetByNameAsync(string name)
            => string.IsNullOrWhiteSpace(name)
                ? Task.FromResult<User>(default)
                : _collection.AsQueryable().SingleOrDefaultAsync(x => x.Name == name.ToLowerInvariant());

        public Task AddAsync(User user)
            => _collection.InsertOneAsync(user);

        public Task UpdateAsync(User user)
            => _collection.ReplaceOneAsync(x => x.Id == user.Id, user);
    }
}
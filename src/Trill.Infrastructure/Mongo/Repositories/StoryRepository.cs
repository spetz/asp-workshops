using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Trill.Core.Entities;
using Trill.Core.Repositories;

namespace Trill.Infrastructure.Mongo.Repositories
{
    internal class StoryRepository : IStoryRepository
    {
        private readonly IMongoCollection<Story> _collection;

        public StoryRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Story>("stories");
        }

        public Task<Story> GetAsync(Guid id)
            => _collection.AsQueryable().SingleOrDefaultAsync(d => d.Id == id);

        public async Task<IEnumerable<Story>> BrowseAsync(Expression<Func<Story, bool>> expression)
            => await _collection
                .AsQueryable()
                .Where(expression)
                .ToListAsync();

        public Task AddAsync(Story story)
            => _collection.InsertOneAsync(story);
    }
}
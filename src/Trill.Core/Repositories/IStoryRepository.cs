using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Trill.Core.Entities;

namespace Trill.Core.Repositories
{
    public interface IStoryRepository
    {
        Task<Story> GetAsync(Guid id);
        Task<IEnumerable<Story>> BrowseAsync(Expression<Func<Story, bool>> expression);
        Task AddAsync(Story story);
    }
}
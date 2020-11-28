using System;
using System.Threading.Tasks;
using Trill.Core.Entities;

namespace Trill.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByNameAsync(string name);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
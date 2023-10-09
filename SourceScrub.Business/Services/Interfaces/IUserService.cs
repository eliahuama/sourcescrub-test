using SourceScrub.Entities;

namespace SourceScrub.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetAsync(int id);
        Task<User> AddAsync(User question);
    }
 }

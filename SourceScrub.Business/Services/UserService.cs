using Microsoft.EntityFrameworkCore;
using SourceScrub.Business.Services.Interfaces;
using SourceScrub.Data;
using SourceScrub.Entities;

namespace SourceScrub.Business.Services
{
    public class UserService: IUserService
    {
        private readonly IRepository<User> _userRepository;

        private readonly ApplicationDbContext _context;

        public UserService(
            IRepository<User> userRepository,
            ApplicationDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public Task<User?> GetAsync(int id)
        {
            return _userRepository.GetAsync(id);
        }

        public Task<User> AddAsync(User user)
        {
            return _userRepository.AddAsync(user);
        }
    }
}

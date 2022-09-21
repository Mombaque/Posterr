using Infra.Core;
using Microsoft.EntityFrameworkCore;
using StriderBackend.Domain.Models;
using StriderBackend.Domain.Repositories;
using StriderBackend.InfraData.Context;

namespace StriderBackend.InfraData.Repositories
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<UserFollower> GetUserFollowers(int userId)
        {
            return _context.UserFollower
                .Include(x => x.Follower)
                .Where(x => x.UserId == userId);
        }

        public User GetUserWithPosts(int userId)
        {
            return DbSet
                .Include(x => x.Posts)
                .FirstOrDefault(x => x.Id == userId);
        }
    }
}

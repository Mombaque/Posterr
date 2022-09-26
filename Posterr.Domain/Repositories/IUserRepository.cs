using Domain.Core.Repository;
using Posterr.Domain.Models;

namespace Posterr.Domain.Repositories
{
    public interface IUserRepository : IRepository<User, int>
    {
        User GetUserWithPosts(int userId);
        IQueryable<UserFollower> GetUserFollowers(int userId);
    }
}

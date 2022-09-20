using Domain.Core.Repository;
using StriderBackend.Domain.Models;

namespace StriderBackend.Domain.Repositories
{
    public interface IUserRepository : IRepository<User, int>
    {
        User GetUserWithPosts(int userId);
    }
}

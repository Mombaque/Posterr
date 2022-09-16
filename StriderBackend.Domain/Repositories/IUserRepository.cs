using StriderBackend.Domain.Models;

namespace StriderBackend.Domain.Repositories
{
    public interface IUserRepository
    {
        User GetUserWithPosts(Guid userId);
    }
}

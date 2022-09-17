using StriderBackend.Domain.Models;

namespace StriderBackend.Domain.Repositories
{
    public interface IPostRepository
    {
        IQueryable<Post> GetUserPosts(int userId, int quantity = 5, int page = 1);
    }
}

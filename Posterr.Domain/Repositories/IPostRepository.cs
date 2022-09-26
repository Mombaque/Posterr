using Posterr.Domain.Models;

namespace Posterr.Domain.Repositories
{
    public interface IPostRepository
    {
        IQueryable<Post> GetUserPosts(int userId, int quantity = 5, int page = 1);
    }
}

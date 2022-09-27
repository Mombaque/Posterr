using Posterr.Domain.Models;

namespace Posterr.Domain.Repositories
{
    public interface IPostRepository
    {
        IQueryable<Post> GetUserPosts(int userId, int quantity = 5, int page = 1);
        IQueryable<Post> GetPosts(GetPostsFilter filter);
    }

    public class GetPostsFilter
    {
        public DateTime CurrentDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public int UserId { get; set; }
    }
}

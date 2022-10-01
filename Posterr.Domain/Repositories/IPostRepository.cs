using Domain.Core.Repository;
using Posterr.Domain.Models;

namespace Posterr.Domain.Repositories
{
    public interface IPostRepository : IRepository<Post, Guid>
    {
        IQueryable<Post> GetUserPosts(GetPostsFilter filter);
        IQueryable<Post> GetPosts(GetPostsFilter filter);
    }

    public class GetPostsFilter
    {
        public DateTime CurrentDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinalDate { get; set; }
        public int UserId { get; set; }
        public int Page { get; set; }
        public int Quantity { get; set; }
    }
}

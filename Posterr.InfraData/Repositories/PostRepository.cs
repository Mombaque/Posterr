using Infra.Core;
using Posterr.Domain.Models;
using Posterr.Domain.Repositories;
using Posterr.InfraData.Context;

namespace Posterr.InfraData.Repositories
{
    public class PostRepository : Repository<Post, Guid>, IPostRepository
    {
        public PostRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<Post> GetPosts(GetPostsFilter filter)
        {
            var posts = DbSet.AsQueryable();

            if (filter.StartDate.HasValue)
                posts = posts.Where(x => x.Date >= filter.StartDate.Value);

            if (filter.FinalDate.HasValue)
                posts = posts.Where(x => x.Date <= filter.FinalDate);

            if (filter.UserId > 0)
                posts = posts.Where(x => x.UserId == filter.UserId);

            return posts;
        }

        public IQueryable<Post> GetUserPosts(int userId, int quantity = 10, int page = 1) =>
            DbSet.Where(x => x.UserId == userId)
                .OrderBy(x => x.Date)
                .Skip((page - 1) * quantity)
                .Take(quantity);
    }
}

using Infra.Core;
using Microsoft.EntityFrameworkCore;
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
            var posts = DbSet
                .Include(x => x.Repost).AsQueryable();

            if (filter.StartDate.HasValue)
                posts = posts.Where(x => x.Date >= filter.StartDate.Value);

            if (filter.FinalDate.HasValue)
                posts = posts.Where(x => x.Date <= filter.FinalDate);

            if (filter.UserId > 0)
                posts = posts.Where(x => x.UserId == filter.UserId);

            return posts;
        }

        public IQueryable<Post> GetUserPosts(GetPostsFilter filter) =>
            DbSet
                .Include(x => x.Repost)
                .Where(x => x.UserId == filter.UserId)
                .OrderBy(x => x.Date)
                .Skip((filter.Page - 1) * filter.Quantity)
                .Take(filter.Quantity);
    }
}

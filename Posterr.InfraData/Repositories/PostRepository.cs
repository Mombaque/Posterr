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

        public IQueryable<Post> GetUserPosts(int userId, int quantity = 5, int page = 1) =>
            DbSet.Where(x => x.UserId == userId)
                .OrderBy(x => x.Date)
                .Skip((page - 1) * quantity)
                .Take(quantity);
    }
}

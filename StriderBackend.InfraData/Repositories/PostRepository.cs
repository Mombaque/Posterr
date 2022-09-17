using Infra.Core;
using StriderBackend.Domain.Models;
using StriderBackend.Domain.Repositories;
using StriderBackend.InfraData.Context;

namespace StriderBackend.InfraData.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
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

using Infra.Core;
using Microsoft.EntityFrameworkCore;
using StriderBackend.Domain.Models;
using StriderBackend.Domain.Repositories;

namespace StriderBackend.InfraData.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DbContext context) : base(context)
        {
        }

        public IQueryable<Post> GetUserPosts(Guid userId, int quantity = 5, int page = 1) => 
            DbSet.Where(x => x.UserId == userId)
                .OrderBy(x => x.Date)
                .Take(quantity)
                .Skip(page - 1 * quantity);
    }
}

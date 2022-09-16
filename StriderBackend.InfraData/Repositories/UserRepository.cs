using Infra.Core;
using Microsoft.EntityFrameworkCore;
using StriderBackend.Domain.Models;
using StriderBackend.Domain.Repositories;

namespace StriderBackend.InfraData.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public User GetUserWithPosts(Guid userId)
        {
            return DbSet
                .Include(x => x.Posts)
                .FirstOrDefault(x => x.Id == userId);
        }
    }
}

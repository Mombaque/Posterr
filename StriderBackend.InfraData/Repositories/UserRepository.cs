using Infra.Core;
using Microsoft.EntityFrameworkCore;
using StriderBackend.Domain.Models;
using StriderBackend.Domain.Repositories;
using StriderBackend.InfraData.Context;

namespace StriderBackend.InfraData.Repositories
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public User GetUserWithPosts(int userId)
        {
            return DbSet
                .Include(x => x.Posts)
                .FirstOrDefault(x => x.Id == userId);
        }
    }
}

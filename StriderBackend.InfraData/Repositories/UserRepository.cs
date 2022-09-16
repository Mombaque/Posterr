using Domain.Core.Repository;
using Infra.Core;
using Microsoft.EntityFrameworkCore;
using StriderBackend.Domain.Models;
using StriderBackend.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using StriderBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriderBackend.Domain.Repositories
{
    public interface IUserRepository
    {
        User GetUserWithPosts(Guid userId);
    }
}

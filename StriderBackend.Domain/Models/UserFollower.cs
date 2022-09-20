using Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriderBackend.Domain.Models
{
    public class UserFollower : Entity<Guid>
    {
        public Guid UserId { get; private set; }
        public Guid UserFollowerId { get; private set; }
    }
}

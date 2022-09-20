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
        public UserFollower(Guid userId, Guid userFollowerId)
        {
            UserId = userId;
            UserFollowerId = userFollowerId;
        }

        public Guid UserId { get; protected set; }
        public Guid UserFollowerId { get; protected set; }

        public User? Follower { get; protected set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriderBackend.Domain.Commands.User
{
    public class FollowUserCommand
    {
        public int UserId { get; private set; }
        public int UserFollowerId { get; private set; }
    }
}

using StriderBackend.Domain.Commands.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriderBackend.Test.Builders.Commands
{
    public class FollowUserCommandBuilder : FollowUserCommand
    {
        public FollowUserCommandBuilder() : base(default, default)
        {}

        public FollowUserCommandBuilder DefaultAndValid()
        {
            UserId = 15;
            UserFollowerId = 20;
            return this;
        }
    }
}

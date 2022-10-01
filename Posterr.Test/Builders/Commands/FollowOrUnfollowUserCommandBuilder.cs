using Posterr.Domain.Commands.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posterr.Test.Builders.Commands
{
    public class FollowOrUnfollowUserCommandBuilder : FollowOrUnfollowUserCommand
    {
        public FollowOrUnfollowUserCommandBuilder() : base(default, default)
        {}

        public FollowOrUnfollowUserCommandBuilder DefaultAndValid()
        {
            UserId = 15;
            UserFollowerId = 20;

            return this;
        }
    }
}

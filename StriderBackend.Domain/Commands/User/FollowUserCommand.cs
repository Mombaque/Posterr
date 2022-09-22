﻿using Domain.Core.Commands;
using StriderBackend.Domain.Commands.User.Validations;

namespace StriderBackend.Domain.Commands.User
{
    public class FollowUserCommand : Command<bool>
    {
        public FollowUserCommand(int userId, int userFollowerId)
        {
            UserId = userId;
            UserFollowerId = userFollowerId;
        }

        public int UserId { get; protected set; }
        public int UserFollowerId { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new FollowUserCommandValidation<FollowUserCommand>().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

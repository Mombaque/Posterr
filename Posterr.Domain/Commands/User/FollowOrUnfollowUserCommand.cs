using Domain.Core.Commands;
using Posterr.Domain.Commands.User.Validations;

namespace Posterr.Domain.Commands.User
{
    public class FollowOrUnfollowUserCommand : Command<bool>
    {
        public FollowOrUnfollowUserCommand(int userId, int userFollowerId)
        {
            UserId = userId;
            UserFollowerId = userFollowerId;
        }

        public int UserId { get; protected set; }
        public int UserFollowerId { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new FollowUserCommandValidation<FollowOrUnfollowUserCommand>().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

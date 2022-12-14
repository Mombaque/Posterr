using FluentValidation;

namespace Posterr.Domain.Commands.User.Validations
{
    public class FollowUserCommandValidation<T> : AbstractValidator<T> where T : FollowOrUnfollowUserCommand
    {
        public FollowUserCommandValidation()
        {
            Validate();
        }

        private void Validate()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.UserFollowerId).NotEmpty().NotNull();
        }
    }
}

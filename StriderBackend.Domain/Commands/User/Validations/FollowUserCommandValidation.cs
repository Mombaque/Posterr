using FluentValidation;

namespace StriderBackend.Domain.Commands.User.Validations
{
    public class FollowUserCommandValidation<T> : AbstractValidator<T> where T : FollowUserCommand
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

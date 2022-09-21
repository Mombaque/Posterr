using FluentValidation;

namespace StriderBackend.Domain.Commands.User.Validations
{
    public class SavePostCommandValidation<T> : AbstractValidator<T> where T : SavePostCommand
    {
        public SavePostCommandValidation()
        {
            Validate();
        }

        private void Validate()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.Content).NotEmpty().NotNull();
            RuleFor(x => x.Date).NotEmpty().NotNull().GreaterThanOrEqualTo(DateTime.UtcNow.Date);
            RuleFor(x => x.Type).IsInEnum();

            RuleFor(x => x.QuoteCommentary)
                .NotEmpty()
                .NotNull()
                .When(x => x.Type == Enums.EPostType.Quote);
        }
    }
}

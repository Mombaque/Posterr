using Domain.Core.Commands;
using Posterr.Domain.Commands.User.Validations;
using Posterr.Domain.Enums;

namespace Posterr.Domain.Commands.User
{
    public class SavePostCommand : Command<bool>
    {
        public SavePostCommand(int userId, string content, DateTime date, EPostType type, Guid repostId, string quoteCommentary)
        {
            UserId = userId;
            Content = content;
            Date = date;
            Type = type;
            RepostId = repostId;
            QuoteCommentary = quoteCommentary;
        }

        public int UserId { get; protected set; }
        public string Content { get; protected set; }
        public DateTime Date { get; protected set; }
        public EPostType Type { get; protected set; }
        public Guid RepostId { get; protected set; }
        public string QuoteCommentary { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new SavePostCommandValidation<SavePostCommand>().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}

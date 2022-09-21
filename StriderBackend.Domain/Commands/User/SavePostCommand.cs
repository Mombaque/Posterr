using Domain.Core.Commands;
using StriderBackend.Domain.Commands.User.Validations;
using StriderBackend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriderBackend.Domain.Commands.User
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

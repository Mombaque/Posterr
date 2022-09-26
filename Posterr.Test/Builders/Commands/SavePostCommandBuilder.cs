using Posterr.Domain.Commands.User;
using Posterr.Domain.Enums;
using System;

namespace Posterr.Test.Builders.Commands
{
    public class SavePostCommandBuilder : SavePostCommand
    {
        public SavePostCommandBuilder() : base(default, default, default, default, default, default)
        {
        }

        public SavePostCommandBuilder DefaultAndValid(EPostType type = EPostType.Regular)
        {
            Content = "Sed ut perspiciatis sit voluptatem accusantium doloremque laudantium";
            UserId = new Random().Next(1, 9999);
            Date = DateTime.Now.Date.AddHours(23).AddMinutes(59);
            Type = type;

            if (Type == EPostType.Quote)
                QuoteCommentary = "voluptatem accusantium";

            if (Type == EPostType.Quote || Type == EPostType.Repost)
                RepostId = Guid.NewGuid();

            return this;
        }

        public SavePostCommandBuilder WithRepostId(Guid id, string content = "Sed ut perspiciatis")
        {
            RepostId = id;
            Content = content;
            return this;
        }

        public SavePostCommandBuilder WithQuotePost(string content)
        {
            RepostId = Guid.NewGuid();
            QuoteCommentary = content;
            Type = EPostType.Quote;
            return this;
        }
    }
}

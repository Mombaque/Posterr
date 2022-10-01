using Domain.Core.Models;
using Posterr.Domain.Enums;

namespace Posterr.Domain.Models
{
    public class Post : Entity<Guid>
    {
        public Post(string content, DateTime date, int userId, EPostType postType, Guid? repostId = default, string? quoteCommentary = default)
        {
            Content = content;
            Date = date;
            UserId = userId;
            Type = postType;
            RepostId = repostId;
            QuoteCommentary = quoteCommentary;
        }

        public Post()
        {

        }

        public string Content { get; protected set; }
        public DateTime Date { get; protected set; }
        public int UserId { get; protected set; }
        public EPostType Type { get; protected set; }
        public Guid? RepostId { get; protected set; }
        public string? QuoteCommentary { get; protected set; }

        public User? User { get; protected set; }
        public Post? Repost { get; protected set; }

        public void AddRepost(Guid repostId, string quoteCommentary)
        {
            RepostId = repostId;
            QuoteCommentary = quoteCommentary ?? string.Empty;
        }
    }
}

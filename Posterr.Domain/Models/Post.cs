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
        public Guid? RepostId { get; private set; }
        public string? QuoteCommentary { get; private set; }

        public User? User { get; protected set; }
        public Post? Repost { get; private set; }

        public void AddRepost(Guid repostId)
        {
            RepostId = repostId;
        }
    }
}

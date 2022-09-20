using Domain.Core.Models;
using StriderBackend.Domain.Enums;

namespace StriderBackend.Domain.Models
{
    public class Post : Entity<Guid>
    {
        public Post(string content, DateTime date, int userId, EPostType postType)
        {
            Content = content;
            Date = date;
            UserId = userId;
            PostType = postType;
        }

        public Post()
        {

        }

        public string Content { get; protected set; }
        public DateTime Date { get; protected set; }
        public int UserId { get; protected set; }
        public EPostType PostType { get; protected set; }

        public User? User { get; protected set; }
    }
}

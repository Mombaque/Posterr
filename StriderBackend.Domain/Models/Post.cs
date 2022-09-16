using Domain.Core.Models;

namespace StriderBackend.Domain.Models
{
    public class Post : Entity
    {
        public Post(string content, DateTime date, Guid userId)
        {
            Content = content;
            Date = date;
            UserId = userId;
        }

        public Post()
        {

        }

        public string Content { get; protected set; }
        public DateTime Date { get; protected set; }
        public Guid UserId { get; protected set; }

        public User? User { get; protected set; }
    }
}

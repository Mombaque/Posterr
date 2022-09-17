using Domain.Core.Models;

namespace StriderBackend.Domain.Models
{
    public class User: Entity<int>
    {
        public User(string name, DateTime creationDate)
        {
            Name = name;
            CreationDate = creationDate;
        }

        public User()
        {

        }

        public string Name { get; protected set; }
        public DateTime CreationDate { get; protected set; }

        public List<Post> Posts { get; protected set; }

        public void AddPost(Post post)
        {
            if(Posts == null)
                Posts = new List<Post>();
            Posts.Add(post);
        }

        public bool PostsLimitReached(DateTime date) => 
            Posts.Count(x => x.Date == date) >= 5;

    }
}

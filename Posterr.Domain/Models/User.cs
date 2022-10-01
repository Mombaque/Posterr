using Domain.Core.Models;
using System;

namespace Posterr.Domain.Models
{
    public class User: Entity<int>
    {
        private const int POST_LIMIT = 5;
        public User(string name, DateTime creationDate)
        {
            Name = name;
            CreationDate = creationDate;
        }

        public User()
        {}

        public string Name { get; protected set; }
        public DateTime CreationDate { get; protected set; }

        public List<Post> Posts { get; protected set; }
        public List<User> Followers { get; protected set; }

        public void AddPost(Post post)
        {
            if(Posts == null)
                Posts = new List<Post>();
            Posts.Add(post);
        }

        public void AddFollower(User userFollower)
        {
            if (Followers == null)
                Followers = new List<User>();
            Followers.Add(userFollower);
        }

        public void RemoveFollower(User user)
        {
            if (Followers == null)
                return;
            Followers.Remove(user);
        }

        public bool PostsLimitReached(DateTime date)
        {
            if (Posts == null)
                return false;

            return Posts?.Count(x => x.Date.Date == date.Date) >= POST_LIMIT;
        }
    }
}

using Posterr.Domain.Models;
using System;
using System.Collections.Generic;

namespace Posterr.Test.Builders.Models
{
    public class UserBuilder : User
    {
        public static UserBuilder Default() => new UserBuilder().DefaultAndValid();

        public UserBuilder() : base(default, default){}


        public UserBuilder DefaultAndValid(int id = default)
        {
            Id = id;
            Name = "John Petrucci";
            CreationDate = DateTime.Now;
            
            Posts = new List<Post>() 
            {
                new PostBuilder().DefaultAndValid(userId: Id) 
            };

            return this;
        }

        public UserBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public UserBuilder WithPosts(List<Post> posts)
        {
            Posts = posts;
            return this;
        }

        public UserBuilder WithFollower(UserBuilder follower)
        {
            AddFollower(follower);
            return this;
        }
    }
}

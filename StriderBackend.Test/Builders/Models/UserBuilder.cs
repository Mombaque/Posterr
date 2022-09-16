using StriderBackend.Domain.Models;
using System;
using System.Collections.Generic;

namespace StriderBackend.Test.Builders.Models
{
    public class UserBuilder : User
    {
        public static UserBuilder Default() => new UserBuilder().DefaultAndValid();

        public UserBuilder() : base(default, default){}


        public UserBuilder DefaultAndValid(Guid? id = default)
        {
            Id = id ?? Guid.NewGuid();
            Name = "John Petrucci";
            CreationDate = DateTime.Now;
            
            Posts = new List<Post>() 
            {
                new PostBuilder().DefaultAndValid(userId: Id) 
            };

            return this;
        }


    }
}

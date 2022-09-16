using StriderBackend.Domain.Models;
using System;

namespace StriderBackend.Test.Builders.Models
{
    public class PostBuilder : Post
    {
        public PostBuilder() : base(default, default, default){}

        public PostBuilder DefaultAndValid(Guid? id = null, Guid? userId = null)
        {
            Id = id ?? Guid.NewGuid();
            Date = DateTime.Now;
            Content = "Some message";
            UserId = userId ?? Guid.NewGuid();

            return this;
        }

        public PostBuilder WithDate(DateTime date)
        {
            Date = date;
            return this;
        }
    }
}

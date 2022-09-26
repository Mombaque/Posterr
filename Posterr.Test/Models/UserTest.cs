using Posterr.Domain.Models;
using Posterr.Test.Builders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Posterr.Test.Models
{
    public class UserTest
    {
        [Fact]
        public void Should_Return_True_When_Post_Limit_Reached()
        {
            var date = DateTime.Now.Date;

            var posts = new List<Post>
            {
                new PostBuilder().DefaultAndValid().WithDate(date.AddMinutes(1)),
                new PostBuilder().DefaultAndValid().WithDate(date.AddMinutes(2)),
                new PostBuilder().DefaultAndValid().WithDate(date.AddMinutes(3)),
                new PostBuilder().DefaultAndValid().WithDate(date.AddMinutes(4)),
                new PostBuilder().DefaultAndValid().WithDate(date.AddMinutes(5)),
                new PostBuilder().DefaultAndValid().WithDate(date.AddMinutes(7)),
                new PostBuilder().DefaultAndValid().WithDate(date.AddMinutes(8)),
            };

            var user = new UserBuilder().DefaultAndValid().WithPosts(posts);

            var result = user.PostsLimitReached(date);
            Assert.True(result);
        }

        [Fact]
        public void Should_Return_False_When_Post_Limit_Not_Reached()
        {
            var date = DateTime.Now.Date;

            var posts = new List<Post>
            {
                new PostBuilder().DefaultAndValid().WithDate(date.AddMinutes(1)),
                new PostBuilder().DefaultAndValid().WithDate(date.AddMinutes(5)),
                new PostBuilder().DefaultAndValid().WithDate(date.AddMinutes(7)),
                new PostBuilder().DefaultAndValid().WithDate(date.AddMinutes(8)),
            };

            var user = new UserBuilder().DefaultAndValid().WithPosts(posts);

            var result = user.PostsLimitReached(date);
            Assert.False(result);
        }
    }
}

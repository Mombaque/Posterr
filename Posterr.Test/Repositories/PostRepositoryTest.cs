using Infra.Core;
using Posterr.Domain.Models;
using Posterr.Domain.Repositories;
using Posterr.InfraData.Context;
using Posterr.InfraData.Repositories;
using Posterr.Test.Builders;
using Posterr.Test.Builders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Posterr.Test.Repositories
{
    public class PostRepositoryTest
    {
        private readonly DataContext _context;
        private readonly UnitOfWork<DataContext> _uow;
        private readonly PostRepository _repository;

        public PostRepositoryTest()
        {
            _context = DataContextBuilder.GetDataContext();
            _uow = new UnitOfWork<DataContext>(_context);
            _repository = new PostRepository(_context);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Shoud_Get_Posts_By_UserId_With_Pagination(int page)
        {
            var userId = 4;
            var startingDate = DateTime.Now.Date.AddHours(8);
            var quantity = page == 1 ? 3 : 2;

            var posts = new List<Post>
            {
                new PostBuilder().DefaultAndValid(userId: userId).WithDate(startingDate.AddMinutes(1)),
                new PostBuilder().DefaultAndValid(userId: userId).WithDate(startingDate.AddMinutes(3)),
                new PostBuilder().DefaultAndValid(userId: userId).WithDate(startingDate.AddMinutes(5)),
                new PostBuilder().DefaultAndValid(userId: userId).WithDate(startingDate.AddMinutes(6)),
                new PostBuilder().DefaultAndValid(userId: userId).WithDate(startingDate.AddMinutes(8)),
                new PostBuilder().DefaultAndValid(),
                new PostBuilder().DefaultAndValid(),
            };

            _context.AddRange(posts);
            _uow.Commit();

            var filter = new GetPostsFilter
            {
                UserId = userId,
                Quantity = quantity,
                Page = page
            };
            var result = _repository.GetUserPosts(filter);

            Assert.True(result.All(x => x.UserId == userId));
            Assert.Equal(quantity, result.Count());
        }

        [Fact]
        public void Should_Get_Posts_With_UserId_Filter()
        {
            var userId1 = 16;
            var userId2 = 11;
            var date = DateTime.Now.Date;

            var postsUser1 = new List<Post>
            {
                new PostBuilder().DefaultAndValid(userId1).WithDate(date),
                new PostBuilder().DefaultAndValid(userId1).WithDate(date.AddHours(1)),
                new PostBuilder().DefaultAndValid(userId1).WithDate(date.AddHours(12)),
                new PostBuilder().DefaultAndValid(userId1).WithDate(date.AddDays(1).AddHours(2)),
                new PostBuilder().DefaultAndValid(userId1).WithDate(date.AddDays(1).AddHours(4)),
                new PostBuilder().DefaultAndValid(userId1).WithDate(date.AddDays(2).AddHours(1)),
                new PostBuilder().DefaultAndValid(userId1).WithDate(date.AddDays(2).AddHours(6)),
            };

            var postsUser2 = new List<Post>
            {
                new PostBuilder().DefaultAndValid(userId2).WithDate(date),
                new PostBuilder().DefaultAndValid(userId2).WithDate(date.AddHours(1)),
                new PostBuilder().DefaultAndValid(userId2).WithDate(date.AddHours(12)),
                new PostBuilder().DefaultAndValid(userId2).WithDate(date.AddDays(2).AddHours(1)),
                new PostBuilder().DefaultAndValid(userId2).WithDate(date.AddDays(2).AddHours(6)),
            };

            _context.AddRange(postsUser1);
            _context.AddRange(postsUser2);
            _uow.Commit();

            var filter = new GetPostsFilter
            {
                UserId = userId1,
                StartDate = date.AddHours(13),
                FinalDate = date.AddDays(2).AddHours(4)
            };

            var result = _repository.GetPosts(filter);

            Assert.Equal(3, result.Count());
            Assert.True(result.All(x => x.UserId == filter.UserId));
        }
    }
}

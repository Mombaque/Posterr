using Infra.Core;
using Posterr.Domain.Models;
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

            var result = _repository.GetUserPosts(userId, quantity, page);

            Assert.True(result.All(x => x.UserId == userId));
            Assert.Equal(quantity, result.Count());
        }
    }
}

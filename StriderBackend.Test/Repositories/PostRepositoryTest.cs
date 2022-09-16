using Infra.Core;
using StriderBackend.Domain.Models;
using StriderBackend.InfraData.Context;
using StriderBackend.InfraData.Repositories;
using StriderBackend.Test.Builders;
using StriderBackend.Test.Builders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StriderBackend.Test.Repositories
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
            var userId = Guid.NewGuid();
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

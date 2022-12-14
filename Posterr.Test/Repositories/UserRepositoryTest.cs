using Infra.Core;
using Posterr.Domain.Models;
using Posterr.InfraData.Context;
using Posterr.InfraData.Repositories;
using Posterr.Test.Builders;
using Posterr.Test.Builders.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Posterr.Test.Repositories
{
    public class UserRepositoryTest
    {
        private readonly DataContext _context;
        private readonly UnitOfWork<DataContext> _uow;
        private readonly UserRepository _repository;

        public UserRepositoryTest()
        {
            _context = DataContextBuilder.GetDataContext();
            _uow = new UnitOfWork<DataContext>(_context);
            _repository = new UserRepository(_context);
        }

        [Fact]
        public void Should_Get_User_With_Posts()
        {
            var userId = 5;

            var users = new List<User>
            {
                new UserBuilder().DefaultAndValid(),
                new UserBuilder().DefaultAndValid().WithId(userId),
                new UserBuilder().DefaultAndValid(),
                new UserBuilder().DefaultAndValid(),
                new UserBuilder().DefaultAndValid(),
            };

            _context.AddRange(users);
            _uow.Commit();

            var result = _repository.GetUser(userId);
            Assert.Equal(users[1].Id, result.Id);
        }
    }
}

using Domain.Core.Mediator;
using Domain.Core.Notification;
using Domain.Core.Repository;
using Moq;
using StriderBackend.Domain.Commands.User;
using StriderBackend.Domain.Models;
using StriderBackend.Domain.Repositories;
using StriderBackend.Test.Builders.Commands;
using StriderBackend.Test.Builders.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace StriderBackend.Test.Commands
{
    public class SavePostCommandTest
    {
        private readonly UserCommandHandler _handler;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IMediatorHandler> _mediator;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IPostRepository> _postRepository;
        private readonly Mock<DomainNotificationHandler> _notifications;
        private readonly CancellationToken _cancellationToken;


        public SavePostCommandTest()
        {
            _uow = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediatorHandler>();
            _postRepository = new Mock<IPostRepository>();
            _userRepository = new Mock<IUserRepository>();
            _notifications = new Mock<DomainNotificationHandler>();
            _cancellationToken = new CancellationToken();

            _uow.Setup(x => x.Commit()).Returns(true);

            _handler = new UserCommandHandler(
                _uow.Object,
                _mediator.Object,
                _postRepository.Object,
                _userRepository.Object,
                _notifications.Object);
        }

        [Fact]
        public void Should_Create_Validation_Errors()
        {
            var command = new SavePostCommandBuilder();
            Assert.False(command.IsValid());

            var errors = command.ValidationResult.Errors;
            Assert.Contains(errors, x => x.PropertyName == "Content");
            Assert.Contains(errors, x => x.PropertyName == "UserId");
            Assert.Contains(errors, x => x.PropertyName == "Date");
        }

        [Fact]
        public void Should_Validade_Quote_Commentary_When_Post_Type_Is_Quote_Post()
        {
            var command = new SavePostCommandBuilder().DefaultAndValid().WithQuotePost(string.Empty);
            Assert.False(command.IsValid());

            var errors = command.ValidationResult.Errors;
            Assert.Contains(errors, x => x.PropertyName == "QuoteCommentary");
        }

        [Fact]
        public void Should_Return_True_When_Command_Is_Valid()
        {
            var command = new SavePostCommandBuilder().DefaultAndValid();
            Assert.True(command.IsValid());
        }

        [Fact]
        public async void Should_Return_False_And_Notify_Error_When_User_Not_Found()
        {
            var command = new SavePostCommandBuilder();
            var result = await _handler.Handle(command, _cancellationToken);
            
            Assert.False(result);

            var errors = command.ValidationResult.Errors;
            _mediator.Verify(x => 
                x.SendCommand(It.IsAny<DomainNotification>()), 
                Times.Exactly(errors.Count));

            _uow.Verify(x => x.Commit(), Times.Never);
        }

        [Fact]
        public async void Should_Return_True_When_Post_Added_To_User()
        {
            _userRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new UserBuilder().DefaultAndValid());

            var command = new SavePostCommandBuilder().DefaultAndValid();
            var result = await _handler.Handle(command, _cancellationToken);

            Assert.True(result);

            _mediator.Verify(x => x.SendCommand(It.IsAny<DomainNotification>()), Times.Never);
            _uow.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public async void Should_Return_False_When_User_Reached_Post_Limit_Per_Day()
        {
            var date = DateTime.Now.Date;

            var posts = new List<Post>()
            {
                new PostBuilder().DefaultAndValid().WithDate(date),
                new PostBuilder().DefaultAndValid().WithDate(date),
                new PostBuilder().DefaultAndValid().WithDate(date),
                new PostBuilder().DefaultAndValid().WithDate(date),
                new PostBuilder().DefaultAndValid().WithDate(date),
            };

            _userRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(
                    new UserBuilder().DefaultAndValid()
                        .WithPosts(posts));

            var command = new SavePostCommandBuilder().DefaultAndValid();
            var result = await _handler.Handle(command, _cancellationToken);

            Assert.False(result);

            _mediator.Verify(x => 
                x.SendCommand(It.Is<DomainNotification>(y => 
                    y.Value.Contains("User reached maximum of 5 allowed in this date"))), 
                Times.Once);

            _uow.Verify(x => x.Commit(), Times.Never);
        }
    }
}

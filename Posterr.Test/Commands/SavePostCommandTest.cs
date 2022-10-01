using Domain.Core.Mediator;
using Domain.Core.Notification;
using Domain.Core.Repository;
using Moq;
using Posterr.Domain.Commands.User;
using Posterr.Domain.Models;
using Posterr.Domain.Repositories;
using Posterr.Test.Builders.Commands;
using Posterr.Test.Builders.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace Posterr.Test.Commands
{
    public class SavePostCommandTest
    {
        private readonly UserCommandHandler _handler;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IMediatorHandler> _mediator;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IUserFollowerRepository> _userFollowerRepository;
        private readonly Mock<IPostRepository> _postRepository;
        private readonly Mock<DomainNotificationHandler> _notifications;
        private readonly CancellationToken _cancellationToken;

        public SavePostCommandTest()
        {
            _uow = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediatorHandler>();
            _notifications = new Mock<DomainNotificationHandler>();
            _uow.Setup(x => x.Commit()).Returns(true);
            _cancellationToken = new CancellationToken();

            _postRepository = new Mock<IPostRepository>();
            _userRepository = new Mock<IUserRepository>();
            _userFollowerRepository = new Mock<IUserFollowerRepository>();

            _handler = new UserCommandHandler(
                _uow.Object,
                _mediator.Object,
                _notifications.Object,
                _postRepository.Object,
                _userRepository.Object,
                _userFollowerRepository.Object);
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
        public async void Should_Return_False_When_User_Reached_Post_Limit_Per_Day()
        {
            var date = DateTime.Now.Date;

            var posts = new List<Post>()
            {
                new PostBuilder().DefaultAndValid().WithDate(date),
                new PostBuilder().DefaultAndValid().WithDate(date),
                new PostBuilder().DefaultAndValid().WithDate(date),
                new PostBuilder().DefaultAndValid().WithDate(date),
                new PostBuilder().DefaultAndValid().WithDate(date),            };

            _userRepository.Setup(x => x.GetUser(It.IsAny<int>()))
                .Returns(
                    new UserBuilder().DefaultAndValid()
                        .WithPosts(posts));

            var command = new SavePostCommandBuilder().DefaultAndValid();
            var result = await _handler.Handle(command, _cancellationToken);

            Assert.False(result);

            _mediator.Verify(x => 
                x.SendCommand(It.Is<DomainNotification>(y => 
                    y.Value.Contains("User reached maximum of 5 allowed posts in this date"))), 
                Times.Once);

            _uow.Verify(x => x.Commit(), Times.Never);
        }

        [Fact]
        public async void Should_Return_False_When_Post_Not_Found()
        {
            var date = DateTime.Now.Date;

            _userRepository.Setup(x => x.GetUser(It.IsAny<int>()))
                .Returns(new UserBuilder().DefaultAndValid());

            var command = new SavePostCommandBuilder().DefaultAndValid().WithRepostId(Guid.NewGuid());
            var result = await _handler.Handle(command, _cancellationToken);

            Assert.False(result);

            _mediator.Verify(x =>
                x.SendCommand(It.Is<DomainNotification>(y =>
                    y.Value.Contains("Post not found with the repostId informed"))),
                Times.Once);

            _uow.Verify(x => x.Commit(), Times.Never);
        }
         
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void Should_Return_True_When_Post_Added_To_User(bool isRepost)
        {
            var command = new SavePostCommandBuilder().DefaultAndValid();
            
            _userRepository.Setup(x => x.GetUser(It.IsAny<int>()))
                .Returns(new UserBuilder().DefaultAndValid());

            if (isRepost)
            {
                var respost = new PostBuilder().DefaultAndValid();
                _postRepository.Setup(x => x.GetById(It.IsAny<Guid>()))
                    .Returns(respost);

                command.WithRepostId(respost.Id);
            }

            var result = await _handler.Handle(command, _cancellationToken);

            Assert.True(result);

            _mediator.Verify(x => x.SendCommand(It.IsAny<DomainNotification>()), Times.Never);
            _uow.Verify(x => x.Commit(), Times.Once);
        }
    }
}

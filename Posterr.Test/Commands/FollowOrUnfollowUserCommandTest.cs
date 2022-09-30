using Domain.Core.Mediator;
using Domain.Core.Notification;
using Domain.Core.Repository;
using Moq;
using Posterr.Domain.Commands.User;
using Posterr.Domain.Repositories;
using Posterr.Test.Builders.Commands;
using Posterr.Test.Builders.Models;
using System.Threading;
using Xunit;

namespace Posterr.Test.Commands
{
    public class FollowOrUnfollowUserCommandTest
    {
        private readonly UserCommandHandler _handler;
        private readonly Mock<IUnitOfWork> _uow;
        private readonly Mock<IMediatorHandler> _mediator;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IPostRepository> _postRepository;
        private readonly Mock<DomainNotificationHandler> _notifications;
        private readonly CancellationToken _cancellationToken;

        public FollowOrUnfollowUserCommandTest()
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
            var command = new FollowOrUnfollowUserCommandBuilder();
            Assert.False(command.IsValid());

            var errors = command.ValidationResult.Errors;
            Assert.Contains(errors, x => x.PropertyName == "UserId");
            Assert.Contains(errors, x => x.PropertyName == "UserFollowerId");
        }

        [Fact]
        public void Should_Return_True_When_Validating_Command()
        {
            var command = new FollowOrUnfollowUserCommandBuilder().DefaultAndValid();
            Assert.True(command.IsValid());
        }

        [Fact]
        public async void Should_Notify_Validation_Errors()
        {
            var command = new FollowOrUnfollowUserCommandBuilder();
            var result = await _handler.Handle(command, _cancellationToken);

            Assert.False(result);

            var errors = command.ValidationResult.Errors;
            _mediator.Verify(x =>
                x.SendCommand(It.IsAny<DomainNotification>()),
                Times.Exactly(errors.Count));

            _uow.Verify(x => x.Commit(), Times.Never);
        }

        [Fact]
        public async void Should_Notify_Errors_When_User_Not_Found()
        {
            var command = new FollowOrUnfollowUserCommandBuilder().DefaultAndValid();

            _userRepository.Setup(x => x.GetById(command.UserFollowerId))
                .Returns(new UserBuilder().DefaultAndValid());

            var result = await _handler.Handle(command, _cancellationToken);

            Assert.False(result);

            _mediator.Verify(x => x.SendCommand(It.Is<DomainNotification>(y => y.Value == "User not found")), Times.Once);
            _uow.Verify(x => x.Commit(), Times.Never);
        }


        [Fact]
        public async void Should_Notify_Errors_When_Follower_Not_Found()
        {
            var command = new FollowOrUnfollowUserCommandBuilder().DefaultAndValid();

            _userRepository.Setup(x => x.GetById(command.UserId))
                .Returns(new UserBuilder().DefaultAndValid());

            var result = await _handler.Handle(command, _cancellationToken);

            Assert.False(result);

            _mediator.Verify(x => x.SendCommand(It.Is<DomainNotification>(y => y.Value == "Follower not found")), Times.Once);
            _uow.Verify(x => x.Commit(), Times.Never);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void Should_Add_Follower(bool follow)
        {
            var command = new FollowOrUnfollowUserCommandBuilder().DefaultAndValid(follow);

            _userRepository.Setup(x => x.GetById(command.UserFollowerId))
                .Returns(new UserBuilder().DefaultAndValid(command.UserFollowerId));

            _userRepository.Setup(x => x.GetById(command.UserId))
                .Returns(new UserBuilder().DefaultAndValid(command.UserId));

            var result = await _handler.Handle(command, _cancellationToken);

            Assert.True(result);
            _mediator.Verify(x => x.SendCommand(It.IsAny<DomainNotification>()), Times.Never);
            _uow.Verify(x => x.Commit(), Times.Once);
        }
    }
}

using Domain.Core.Commands;
using Domain.Core.Mediator;
using Domain.Core.Notification;
using Domain.Core.Repository;
using MediatR;
using Posterr.Domain.Models;
using Posterr.Domain.Repositories;

namespace Posterr.Domain.Commands.User
{
    public class UserCommandHandler : CommandHandler, 
        IRequestHandler<SavePostCommand, bool>,
        IRequestHandler<FollowOrUnfollowUserCommand, bool>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserFollowerRepository _userFollowerRepository;

        public UserCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler mediator,
            IRequestHandler<DomainNotification, bool> notificationHandler,
            IPostRepository postRepository,
            IUserRepository userRepository,
            IUserFollowerRepository userFollowerRepository) : base(uow, mediator, notificationHandler)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _userFollowerRepository = userFollowerRepository;
        }

        public async Task<bool> Handle(SavePostCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var user = _userRepository.GetUser(request.UserId);
            if (user == null)
            {
                NotifyError("User not found");
                return false;
            }

            if (user.PostsLimitReached(request.Date))
            {
                NotifyError("User reached maximum of 5 allowed posts in this date");
                return false;
            }

            var repost = _postRepository.GetById(request.RepostId);
            if (request.RepostId != default && repost == null)
            {
                NotifyError("Post not found with the repostId informed");
                return false;
            }

            var post = new Post(
                request.Content,
                request.Date,
                request.UserId,
                request.Type);

            if (request.RepostId != Guid.Empty)
                post.AddRepost(request.RepostId);

            user.AddPost(post);

            return await Task.FromResult(Commit());
        }

        public async Task<bool> Handle(FollowOrUnfollowUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return false;
            }

            var user = _userRepository.GetById(request.UserId);
            if (user == null)
            {
                NotifyError("User not found");
                return false;
            }

            var userFollower = _userRepository.GetById(request.UserFollowerId);
            if (userFollower == null)
            {
                NotifyError("Follower not found");
                return false;
            }

            var userFollowerEntity = _userFollowerRepository.GetUserFollower(user.Id, userFollower.Id);

            if(userFollowerEntity == null)
                _userFollowerRepository.Add(new UserFollower(user.Id, userFollower.Id));
            else
                _userFollowerRepository.Delete(userFollowerEntity);

            return await Task.FromResult(Commit());
        }
    }
}

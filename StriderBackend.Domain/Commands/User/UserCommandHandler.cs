using Domain.Core.Commands;
using Domain.Core.Mediator;
using Domain.Core.Notification;
using Domain.Core.Repository;
using MediatR;
using StriderBackend.Domain.Models;
using StriderBackend.Domain.Repositories;

namespace StriderBackend.Domain.Commands.User
{
    public class UserCommandHandler : CommandHandler, IRequestHandler<SavePostCommand, bool>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(
            IUnitOfWork uow,
            IMediatorHandler mediator,
            IPostRepository postRepository,
            IUserRepository userRepository,
            IRequestHandler<DomainNotification, bool> notificationHandler) : base(uow, mediator, notificationHandler)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(SavePostCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                NotifyValidationErrors(request);
                return await Task.FromResult(false);
            }

            var user = _userRepository.GetById(request.UserId);
            if (user == null)
            {
                NotifyError("User not found");
                return await Task.FromResult(false);
            }

            if(user.PostsLimitReached(request.Date))
            {
                NotifyError("User reached maximum of 5 allowed in this date");
                return await Task.FromResult(false);
            }

            var post = new Post(
                request.Content,
                request.Date,
                request.UserId,
                request.Type);

            user.AddPost(post);

            return await Task.FromResult(Commit());
        }
    }
}

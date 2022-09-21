using Domain.Core.Commands;
using Domain.Core.Mediator;
using Domain.Core.Models;
using Domain.Core.Notification;
using Domain.Core.Repository;
using Domain.Core.Services;
using MediatR;
using StriderBackend.Domain.Models;
using StriderBackend.Domain.Repositories;

namespace StriderBackend.Domain.Commands.User
{
    public class UserCommandHandler : CommandHandler, IRequestHandler<SavePostCommand, bool>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMediatorHandler _mediator;

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

            var post = new Post(
                request.Content,
                request.Date,
                request.UserId,
                request.Type);

            user.AddPost(post);
            _userRepository.Add(user);

            return await Task.FromResult(Commit());
        }
    }
}

using Domain.Core.Mediator;
using Domain.Core.Notification;
using Domain.Core.Repository;
using MediatR;

namespace Domain.Core.Commands
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _mediator;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(
            IUnitOfWork uow,
            IMediatorHandler mediator,
            IRequestHandler<DomainNotification, bool> notifications)
        {
            _uow = uow;
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public bool Commit() => _uow.Commit();

        protected async void NotifyError(string error) =>
            await _mediator.SendCommand(new DomainNotification(error));

        public void NotifyValidationErrors<T>(Command<T> command)
        {
            foreach (var error in command.ValidationResult.Errors)
                NotifyError(error.ErrorMessage);
        }
    }
}

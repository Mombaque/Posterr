using MediatR;

namespace Domain.Core.Notification
{
    public class DomainNotificationHandler : IRequestHandler<DomainNotification, bool>
    {
        private List<DomainNotification> _notifications;
        private readonly object _lockObject = new object();

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public Task<bool> Handle(DomainNotification request, CancellationToken cancellationToken)
        {
            lock (_lockObject)
            {
                _notifications.Add(request);
            }
            return Task.FromResult(true);

        }

        public bool HasErrors() => _notifications.Any();

        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }
    }
}

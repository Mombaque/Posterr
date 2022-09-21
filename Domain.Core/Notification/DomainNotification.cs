using Domain.Core.Commands;

namespace Domain.Core.Notification
{
    public class DomainNotification : Command<bool>
    {
        public DomainNotification(string value)
        {
            Value = value;
        }

        public string Value { get; protected set; }
    }
}

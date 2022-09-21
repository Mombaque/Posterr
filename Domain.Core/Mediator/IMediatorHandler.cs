using Domain.Core.Commands;

namespace Domain.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task<R> SendCommand<R>(Command<R> command);
    }
}

using Domain.Core.Mediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Commands
{
    public class InMemoryMediator : IMediatorHandler
    {

        private readonly IMediator _mediator;

        public InMemoryMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<R> SendCommand<R>(Command<R> command)
        {
            return _mediator.Send(command);

        }
    }
}

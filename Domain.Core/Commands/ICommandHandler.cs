using Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Services
{
    public interface ICommandHandler<in TRequest, TResponse> where TRequest : class
    {
        Task<TResponse> Handle(TRequest command);
    }
}

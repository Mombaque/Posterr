using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Commands
{
    [Serializable]
    public class Command
    {
        public ValidationResult ValidationResult { get; protected set; }
        public virtual bool IsValid() => true;
    }
}

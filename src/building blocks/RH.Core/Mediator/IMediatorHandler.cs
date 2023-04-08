using FluentValidation.Results;
using RH.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
    }
}

using FluentValidation;
using RH.Core.Messages;
using System;

namespace RH.Pagamento.API.Application.Commands
{
    public class CancelarContaCommand : Command
    {           
        public Guid PedidoId { get; private set; }       

        public CancelarContaCommand( Guid pedidoId)
        {           
            PedidoId = pedidoId;           
        }

        public override bool EhValido()
        {
            ValidationResult = new CancelarContaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CancelarContaValidation : AbstractValidator<CancelarContaCommand>
    {
        public static string IdPedidoErroMsg => "Id do pedido inválido";      
        public CancelarContaValidation()
        {           

            RuleFor(c => c.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdPedidoErroMsg);         
        }
    }
}

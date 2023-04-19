using FluentValidation;
using RH.Core.Messages;
using System;

namespace RH.Pedidos.API.Application.Commands
{
    public class CancelarPedidoCommand : Command
    {
        // Pedido
        public Guid PedidoId { get; private set; }

        public CancelarPedidoCommand(Guid pedidoId)
        {
            PedidoId = pedidoId;

        }

        public override bool EhValido()
        {
            ValidationResult = new CancelarPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CancelarPedidoValidation : AbstractValidator<CancelarPedidoCommand>
    {
        public static string IdPedidoErroMsg => "Id do pedido inválido";

        public CancelarPedidoValidation()
        {
            RuleFor(c => c.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdPedidoErroMsg);
        }
    }
}

using FluentValidation;
using RH.Core.Messages;
using System;

namespace RH.Pedidos.API.Application.Commands
{
    public class EmitirPedidoCommand : Command
    {
        // Pedido
        public Guid PedidoId { get; private set; }

        public EmitirPedidoCommand(Guid pedidoId)
        {
            PedidoId = pedidoId;

        }

        public override bool EhValido()
        {
            ValidationResult = new EmitirPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class EmitirPedidoValidation : AbstractValidator<EmitirPedidoCommand>
    {
        public static string IdPedidoErroMsg => "Id do pedido inválido";

        public EmitirPedidoValidation()
        {
            RuleFor(c => c.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdPedidoErroMsg);
        }
    }
}

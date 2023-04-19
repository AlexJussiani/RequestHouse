using FluentValidation;
using RH.Core.Messages;
using System;

namespace RH.Pedidos.API.Application.Commands
{
    public class AutorizarPedidoCommand : Command
    {
        // Pedido
        public Guid PedidoId { get; private set; }

        public AutorizarPedidoCommand(Guid pedidoId)
        {
            PedidoId = pedidoId;

        }

        public override bool EhValido()
        {
            ValidationResult = new AutorizarPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AutorizarPedidoValidation : AbstractValidator<AutorizarPedidoCommand>
    {
        public static string IdPedidoErroMsg => "Id do pedido inválido";

        public AutorizarPedidoValidation()
        {
            RuleFor(c => c.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdPedidoErroMsg);
        }
    }
}

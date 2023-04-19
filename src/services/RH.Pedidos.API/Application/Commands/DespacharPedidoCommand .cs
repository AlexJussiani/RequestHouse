using FluentValidation;
using RH.Core.Messages;
using System;

namespace RH.Pedidos.API.Application.Commands
{
    public class DespacharPedidoCommand : Command
    {
        // Pedido
        public Guid PedidoId { get; private set; }

        public DespacharPedidoCommand(Guid pedidoId)
        {
            PedidoId = pedidoId;

        }

        public override bool EhValido()
        {
            ValidationResult = new DespacharPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class DespacharPedidoValidation : AbstractValidator<DespacharPedidoCommand>
    {
        public static string IdPedidoErroMsg => "Id do pedido inválido";

        public DespacharPedidoValidation()
        {
            RuleFor(c => c.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdPedidoErroMsg);
        }
    }
}

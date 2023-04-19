using FluentValidation;
using RH.Core.Messages;
using System;

namespace RH.Pedidos.API.Application.Commands
{
    public class EntregarPedidoCommand : Command
    {
        // Pedido
        public Guid PedidoId { get; private set; }

        public EntregarPedidoCommand(Guid pedidoId)
        {
            PedidoId = pedidoId;

        }

        public override bool EhValido()
        {
            ValidationResult = new EntregarPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class EntregarPedidoValidation : AbstractValidator<EntregarPedidoCommand>
    {
        public static string IdPedidoErroMsg => "Id do pedido inválido";

        public EntregarPedidoValidation()
        {
            RuleFor(c => c.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdPedidoErroMsg);
        }
    }
}

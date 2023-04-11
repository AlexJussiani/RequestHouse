using FluentValidation;
using RH.Core.Messages;
using System;

namespace RH.Pedidos.API.Application.Commands
{
    public class AdicionarPedidoCommand : Command
    {
        // Pedido
        public Guid ClienteId { get; private set; }

        public AdicionarPedidoCommand(Guid clienteId)
        {
            ClienteId = clienteId;

        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarPedidoValidation : AbstractValidator<AdicionarPedidoCommand>
    {
        public static string IdClienteErroMsg => "Id do cliente inválido";

        public AdicionarPedidoValidation()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdClienteErroMsg);
        }
    }
}

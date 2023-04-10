using FluentValidation;
using RH.Core.Messages;
using RH.Pedidos.Domain;
using System;
using System.Collections.Generic;

namespace RH.Pedidos.API.Application.Commands
{
    public class RemoverItemPedidoCommand : Command
    {
        // Pedido
        public Guid PedidoId { get; private set; }

        public Guid ProdutoId { get; private set; }

        public RemoverItemPedidoCommand(Guid pedidoId, Guid produtoId)
        {
            PedidoId = pedidoId;
            ProdutoId = produtoId;            
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverItemPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoverItemPedidoValidation : AbstractValidator<RemoverItemPedidoCommand>
    {
        public static string IdPedidoErroMsg => "Id do pedido inválido";
        public static string IdProdutoErroMsg => "Id do produto inválido";
        public RemoverItemPedidoValidation()
        {
            RuleFor(c => c.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdPedidoErroMsg);

            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdProdutoErroMsg);           
        }
    }
}
using FluentValidation;
using RH.Core.Messages;
using System;

namespace RH.Pedidos.API.Application.Commands
{
    public class AtualizarValorPedidoCommand : Command
    {
        // Pedido
        public Guid PedidoId { get; private set; }
        public decimal ValorDesconto { get; private set; }
        public decimal ValorAcrescimo { get; private set; }
        public string Observacoes { get; private set; }

        public AtualizarValorPedidoCommand(Guid pedidoId, decimal valorAcrescimo, decimal valorDesconto, string observacoes)
        {
            PedidoId = pedidoId;
            ValorAcrescimo = valorAcrescimo;
            ValorDesconto = valorDesconto;
            Observacoes = observacoes;
        }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarPedidoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AtualizarPedidoValidation : AbstractValidator<AtualizarValorPedidoCommand>
    {
        public static string IdPedidoErroMsg => "Id do pedido inválido";
        public static string ValorDescontoErroMsg => "Valor Desconto não pode ser menor que zero";
        public static string ValorAcrescimoErroMsg => "Valor Acrescimo não pode ser menor que zero";

        public AtualizarPedidoValidation()
        {
            RuleFor(c => c.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdPedidoErroMsg);

            RuleFor(c => c.ValorAcrescimo)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ValorAcrescimoErroMsg);

            RuleFor(c => c.ValorDesconto)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ValorDescontoErroMsg);
        }
    }
}


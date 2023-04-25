using FluentValidation;
using RH.Core.Messages;
using System;

namespace RH.Pagamento.API.Application.Commands
{
    public class RegistrarPagamentoCommand : Command
    {   
        public Guid ContaId { get; private set; }
        public decimal ValorPago { get; private set; }
        public DateTime DataPagamento { get; private set; }

        public RegistrarPagamentoCommand(Guid contaId, decimal valorPago, DateTime dataPagamento)
        {
            ContaId = contaId;
            ValorPago = valorPago;
            DataPagamento = dataPagamento;
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarPagamentoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RegistrarPagamentoValidation : AbstractValidator<RegistrarPagamentoCommand>
    {
        public static string IdContaErroMsg => "Id da conta inválido";
        public static string DataPagamentoErroMsg => "Data de pagamento não informada inválido";
        public static string ValorPagamentoErroMsg => "O valor do pagamento precisa ser maior que 0";

        public RegistrarPagamentoValidation()
        {

            RuleFor(c => c.ContaId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdContaErroMsg);


            RuleFor(c => c.DataPagamento)
                .NotNull()
                .WithMessage(DataPagamentoErroMsg);

            RuleFor(c => c.ValorPago)
                .GreaterThan(0)
                .WithMessage(ValorPagamentoErroMsg);
        }
    }
}

using FluentValidation;
using RH.Core.Messages;
using System;

namespace RH.Pagamento.API.Application.Commands
{
    public class AdicionarContaCommand : Command
    {   
        public int Codigo { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataVencimento { get; private set; }

        public AdicionarContaCommand(int codigo, Guid pedidoId, Guid clienteId, decimal valorTotal)
        {
            Codigo = codigo;
            PedidoId = pedidoId;
            ClienteId = clienteId;
            ValorTotal = valorTotal;
            DataVencimento = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
        }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarContaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarContaValidation : AbstractValidator<AdicionarContaCommand>
    {
        public static string CodigoPedidoErroMsg => "Id do pedido inválido";
        public static string IdPedidoErroMsg => "Id do pedido inválido";
        public static string IdClienteErroMsg => "Id do cliente inválido";
        public static string DataVencimentoErroMsg => "Data de vencimento não informada inválido";
        public static string ValorTotalErroMsg => "O valor da conta precisa ser maior que 0";

        public AdicionarContaValidation()
        {
            RuleFor(c => c.Codigo)
                .NotNull()
                .WithMessage(CodigoPedidoErroMsg);

            RuleFor(c => c.PedidoId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdPedidoErroMsg);

            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage(IdClienteErroMsg);

            RuleFor(c => c.DataVencimento)
                .NotNull()
                .WithMessage(DataVencimentoErroMsg);

            RuleFor(c => c.ValorTotal)
                .GreaterThan(0)
                .WithMessage(ValorTotalErroMsg);
        }
    }
}

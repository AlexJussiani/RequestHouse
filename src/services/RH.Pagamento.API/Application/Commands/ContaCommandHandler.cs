using FluentValidation.Results;
using MediatR;
using RH.Core.DomainObjects;
using RH.Core.Messages;
using RH.Pagamento.API.Application.Events;
using RH.Pagamento.API.Data.Repository;
using RH.Pagamento.API.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Pagamento.API.Application.Commands
{
    public class ContaCommandHandler : CommandHandler,
        IRequestHandler<AdicionarContaCommand, ValidationResult>
    {
        private readonly IMediator _mediator;
        private readonly IPagamentoRepository _PagamentoRepository;

        public ContaCommandHandler(IMediator mediator, IPagamentoRepository pagamentoRepository)
        {
            _mediator = mediator;
            _PagamentoRepository = pagamentoRepository;
        }

        public async Task<ValidationResult> Handle(AdicionarContaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return message.ValidationResult;

            var pedidoExistente = await _PagamentoRepository.ObterContaPorIdPedido(message.PedidoId);

            if(pedidoExistente != null)
            {
                AdicionarErro("Já existe uma conta cadastrada para esse pedido");
                return ValidationResult;
            }

            var conta = new Conta(message.Codigo, message.PedidoId, message.ClienteId, message.ValorTotal, message.DataVencimento);

            _PagamentoRepository.Adicionar(conta);

            conta.AdicionarEvento(new ContaAdicionadaEvent());

            return await (PersistirDados(_PagamentoRepository.UnitOfWork));

        }

        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediator.Publish(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}

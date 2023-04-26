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
        IRequestHandler<AdicionarContaCommand, ValidationResult>,
        IRequestHandler<CancelarContaCommand, ValidationResult>,
        IRequestHandler<RegistrarPagamentoCommand, ValidationResult>
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

            var contaExistente = await _PagamentoRepository.ObterContaPorIdPedido(message.PedidoId);

            if(contaExistente != null)
            {
                AdicionarErro("Já existe uma conta cadastrada para esse pedido");
                return ValidationResult;
            }

            var conta = new Conta(message.Codigo, message.PedidoId, message.ClienteId, message.ValorTotal, message.DataVencimento);

            _PagamentoRepository.AdicionarConta(conta);

            conta.AdicionarEvento(new ContaAdicionadaEvent());

            return await (PersistirDados(_PagamentoRepository.UnitOfWork));

        }

        public async Task<ValidationResult> Handle(CancelarContaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return message.ValidationResult;

            var contaExistente = await _PagamentoRepository.ObterContaPorIdPedido(message.PedidoId);

            if (contaExistente == null)
            {
                AdicionarErro("Pedido Não localizado");
                return ValidationResult;
            }

            contaExistente.CancelarConta();

            _PagamentoRepository.AtualizarConta(contaExistente);

            contaExistente.AdicionarEvento(new ContaAdicionadaEvent());

            return await (PersistirDados(_PagamentoRepository.UnitOfWork));
        }

        public async Task<ValidationResult> Handle(RegistrarPagamentoCommand message, CancellationToken cancellationToken)
        {
            var conta = await _PagamentoRepository.ObterContaPorId(message.ContaId);

            if(conta == null)
            {
                AdicionarErro("Conta não identificada");
                return ValidationResult;
            }

            if (conta.Cancelado)
            {
                AdicionarErro("Não é possivel realizar um pagamento de uma conta cancelada");
                return ValidationResult;
            }

            if (conta.ContaStatus == ContaStatus.Pago)
            {
                AdicionarErro("Essa conta já se encontra paga");
                return ValidationResult;
            }
            var pagamento = new PagamentoConta(message.ContaId, message.ValorPago, message.DataPagamento);
            conta.RealizarPagamento(pagamento);

            _PagamentoRepository.AdicionarPagamento(pagamento);
            _PagamentoRepository.AtualizarConta(conta);

            conta.AdicionarEvento(new PagamentoContaAdicionadaEvent());

            return await(PersistirDados(_PagamentoRepository.UnitOfWork));
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

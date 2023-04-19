using FluentValidation.Results;
using MediatR;
using RH.Core.DomainObjects;
using RH.Core.Messages;
using RH.Pedidos.API.Application.Events;
using RH.Pedidos.Data.Repository;
using RH.Pedidos.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Pedidos.API.Application.Commands
{
    public class PedidoCommandHandler : CommandHandler,
        IRequestHandler<AdicionarItemPedidoCommand, ValidationResult>,
        IRequestHandler<AtualizarItemPedidoCommand, ValidationResult>,
        IRequestHandler<RemoverItemPedidoCommand, ValidationResult>,
        IRequestHandler<AdicionarPedidoCommand, ValidationResult>,
        IRequestHandler<EmitirPedidoCommand, ValidationResult>
    {

        private readonly IMediator _mediator;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository,
                                   IMediator mediator)
        {
            _pedidoRepository = pedidoRepository;
            _mediator = mediator;
        }

        public async Task<ValidationResult> Handle(AdicionarPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return message.ValidationResult;

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(message.ClienteId);

            _pedidoRepository.Adicionar(pedido);

            pedido.AdicionarEvento(new PedidoItemAdicionadoEvent());

            return await PersistirDados(_pedidoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return message.ValidationResult;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorPedidoId(message.PedidoId);
            var pedidoItem = new PedidoItem(message.ProdutoId, message.Nome, message.Quantidade, message.ValorUnitario);

            if(pedido == null)
            {
                // pedido = Pedido.PedidoFactory.NovoPedidoRascunho(message.PedidoId);
                //   pedido.AdicionarItem(pedidoItem);

                // _pedidoRepository.Adicionar(pedido);
                AdicionarErro("Pedido Não Encontrado");
                return ValidationResult;
            }
            else
            {
                var pedidoItemExistente = pedido.ItemExistente(pedidoItem);
                pedido.AdicionarItem(pedidoItem);

                if (pedidoItemExistente)
                {
                    _pedidoRepository.AtualizarItem(pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId));
                }
                else
                {
                    _pedidoRepository.AdicionarItem(pedidoItem);
                }
                _pedidoRepository.Atualizar(pedido);
            }

            pedido.AdicionarEvento(new PedidoItemAdicionadoEvent());

            return await PersistirDados(_pedidoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AtualizarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return message.ValidationResult;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorPedidoId(message.PedidoId);

            if(pedido == null)
            {
                AdicionarErro("Pedido Não Encontrado");
                return ValidationResult;
            }

            var pedidoItem = await _pedidoRepository.ObterItemPorPedido(message.PedidoId, message.ProdutoId);

            if(pedidoItem == null)
            {
                AdicionarErro("Item pedido Não Encontrado");
                return ValidationResult;
            }

            pedido.AtualizarUnidades(pedidoItem, message.Quantidade);
            _pedidoRepository.AtualizarItem(pedidoItem);
            _pedidoRepository.Atualizar(pedido);

            pedido.AdicionarEvento(new PedidoItemAtualizadoEvent());

            return await PersistirDados(_pedidoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(RemoverItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return message.ValidationResult;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorPedidoId(message.PedidoId);

            if (pedido == null)
            {
                AdicionarErro("Pedido Não Encontrado");
                return ValidationResult;
            }

            var pedidoItem = await _pedidoRepository.ObterItemPorPedido(message.PedidoId, message.ProdutoId);

            if (pedidoItem == null)
            {
                AdicionarErro("Item pedido Não Encontrado");
                return ValidationResult;
            }

            pedido.RemoverItem(pedidoItem);

            _pedidoRepository.RemoverItem(pedidoItem);
            _pedidoRepository.Atualizar(pedido);

            pedido.AdicionarEvento(new PedidoItemExcluidoEvent());

            return await PersistirDados(_pedidoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(EmitirPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return message.ValidationResult;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorPedidoId(message.PedidoId);

            if (pedido == null)
            {
                AdicionarErro("Pedido Não Encontrado");
                return ValidationResult;
            }

            if(pedido.PedidoStatus != PedidoStatus.Rascunho)
            {
                AdicionarErro("Para emitir o Pedido ele precisa estar em Rascunho");
                return ValidationResult;
            }

            pedido.EmitirPedido();
            _pedidoRepository.Atualizar(pedido);
            pedido.AdicionarEvento(new PedidoEmitidoEventHandler());

            return await PersistirDados(_pedidoRepository.UnitOfWork);
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

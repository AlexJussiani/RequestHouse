using FluentValidation.Results;
using MediatR;
using RH.Core.DomainObjects;
using RH.Core.Messages;
using RH.Pedidos.API.Application.Events;
using RH.Pedidos.Data.Migrations;
using RH.Pedidos.Data.Repository;
using RH.Pedidos.Domain;
using System;
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
        IRequestHandler<EmitirPedidoCommand, ValidationResult>,
        IRequestHandler<AutorizarPedidoCommand, ValidationResult>,
        IRequestHandler<DespacharPedidoCommand, ValidationResult>,
        IRequestHandler<EntregarPedidoCommand, ValidationResult>,
        IRequestHandler<CancelarPedidoCommand, ValidationResult>
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

            pedido.AdicionarEvento(new PedidoAdicionadoEvent());

            return await PersistirDados(_pedidoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return message.ValidationResult;
            Thread.Sleep(500);
            var pedido = await _pedidoRepository.ObterPorPedidoId(message.PedidoId);
            var pedidoItem = new PedidoItem(message.ProdutoId, message.Nome, message.Quantidade, message.ValorUnitario);

            if(pedido == null)
            {                
                AdicionarErro("Pedido Não Encontrado");
                return ValidationResult;
            }
            if (pedido.PedidoStatus != PedidoStatus.Rascunho)
            {
                AdicionarErro("O Pedido precisa estar em rascunho para adicioanr Itens");
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

            var pedido = await _pedidoRepository.ObterPorPedidoId(message.PedidoId);

            if(pedido == null)
            {
                AdicionarErro("Pedido Não Encontrado");
                return ValidationResult;
            }
            if (pedido.PedidoStatus != PedidoStatus.Rascunho)
            {
                AdicionarErro("O Pedido precisa estar em rascunho para adicionar Itens");
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

            var pedido = await _pedidoRepository.ObterPorPedidoId(message.PedidoId);

            if (pedido == null)
            {
                AdicionarErro("Pedido Não Encontrado");
                return ValidationResult;
            }

            if (pedido.PedidoStatus != PedidoStatus.Rascunho)
            {
                AdicionarErro("O Pedido precisa estar em rascunho para remover itens");
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

            var pedido = await _pedidoRepository.ObterPorPedidoId(message.PedidoId);

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

            if(pedido.PedidoItems.Count == 0)
            {
                AdicionarErro("Pedido precisa ter pelo menos 1 item");
                return ValidationResult;
            }

            pedido.EmitirPedido();
            _pedidoRepository.Atualizar(pedido);
            pedido.AdicionarEvento(new PedidoEmitidoEvent());

            return await PersistirDados(_pedidoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AutorizarPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return message.ValidationResult;

            var pedido = await _pedidoRepository.ObterPorPedidoId(message.PedidoId);

            if (pedido == null)
            {
                AdicionarErro("Pedido Não Encontrado");
                return ValidationResult;
            }

            if (pedido.PedidoStatus != PedidoStatus.Emitido)
            {
                AdicionarErro("Para autorizar o Pedido ele precisa estar emitido");
                return ValidationResult;
            }

            if (pedido.PedidoItems.Count == 0)
            {
                AdicionarErro("Pedido precisa ter pelo menos 1 item");
                return ValidationResult;
            }

            pedido.AutorizarPedido();
            _pedidoRepository.Atualizar(pedido);
            pedido.AdicionarEvento(new PedidoAutorizadoEvent(pedido.Codigo, pedido.Id, pedido.ClienteId, pedido.ValorTotal));

            return await PersistirDados(_pedidoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(DespacharPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return message.ValidationResult;

            var pedido = await _pedidoRepository.ObterPorPedidoId(message.PedidoId);

            if (pedido == null)
            {
                AdicionarErro("Pedido Não Encontrado");
                return ValidationResult;
            }

            if (pedido.PedidoStatus != PedidoStatus.Autorizado)
            {
                AdicionarErro("Para despachar o Pedido ele precisa estar autorizado");
                return ValidationResult;
            }

            if (pedido.PedidoItems.Count == 0)
            {
                AdicionarErro("Pedido precisa ter pelo menos 1 item");
                return ValidationResult;
            }

            pedido.DespacharPedido();
            _pedidoRepository.Atualizar(pedido);
            pedido.AdicionarEvento(new PedidoDespachadoEvent());

            return await PersistirDados(_pedidoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(EntregarPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return message.ValidationResult;

            var pedido = await _pedidoRepository.ObterPorPedidoId(message.PedidoId);

            if (pedido == null)
            {
                AdicionarErro("Pedido Não Encontrado");
                return ValidationResult;
            }

            if (pedido.PedidoStatus != PedidoStatus.Autorizado && pedido.PedidoStatus != PedidoStatus.Percurso)
            {
                AdicionarErro("Para entregar o Pedido ele precisa estar autorizado ou em percurso");
                return ValidationResult;
            }

            if (pedido.PedidoItems.Count == 0)
            {
                AdicionarErro("Pedido precisa ter pelo menos 1 item");
                return ValidationResult;
            }

            pedido.EntregarPedido();
            _pedidoRepository.Atualizar(pedido);
            pedido.AdicionarEvento(new PedidoEntregueEvent());

            return await PersistirDados(_pedidoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(CancelarPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return message.ValidationResult;

            var pedido = await _pedidoRepository.ObterPorPedidoId(message.PedidoId);

            if (pedido == null)
            {
                AdicionarErro("Pedido Não Encontrado");
                return ValidationResult;
            }

            if (pedido.PedidoStatus == PedidoStatus.Entregue || pedido.PedidoStatus == PedidoStatus.Percurso)
            {
                AdicionarErro("Não é possivel cancelar um pedido que está entregue ou em percurso");
                return ValidationResult;
            }

            pedido.CancelarPedido();
            _pedidoRepository.Atualizar(pedido);
            pedido.AdicionarEvento(new PedidoCanceladoEvent(pedido.Id));

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

﻿using FluentValidation.Results;
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
        IRequestHandler<AtualizarItemPedidoCommand, ValidationResult>
    {

        private readonly IMediator _mediator;
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository,
                                   IMediator mediator)
        {
            _pedidoRepository = pedidoRepository;
            _mediator = mediator;
        }

        public async Task<ValidationResult> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return message.ValidationResult;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorPedidoId(message.PedidoId);
            var pedidoItem = new PedidoItem(message.ProdutoId, message.Nome, message.Quantidade, message.ValorUnitario);

            if(pedido == null)
            {
                pedido = Pedido.PedidoFactory.NovoPedidoRascunho(message.PedidoId);
                pedido.AdicionarItem(pedidoItem);

                _pedidoRepository.Adicionar(pedido);
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

            var pedidoItem = await _pedidoRepository.

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

﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using RH.Pedidos.API.Application.Commands;
using RH.Pedidos.API.Application.Queries;
using System.Threading.Tasks;
using System;
using RH.Pedidos.API.Application.Queries.ViewModels;
using RH.Core.Controllers;
using RH.Core.Usuario;
using RH.Core.Identidade;

namespace RH.Pedidos.API.Controllers
{
    public class PedidoController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly IMediator _mediatorHandler;
        private readonly IPedidoQueries _queries;

        public PedidoController(IAspNetUser user, IMediator mediatorHandler, IPedidoQueries queries)
        {
            _user = user;
            _mediatorHandler = mediatorHandler;
            _queries = queries;
        }

        [ClaimsAuthorize("Pedido", "Visualizar")]
        [HttpGet("api/lista-pedidos-autorizados")]
        public async Task<IActionResult> ObterPedidosAutorizados()
        {
            var pedidos = await _queries.ObterListaPedidosAutorizados();
            return pedidos == null ? NotFound() : CustomResponse(pedidos);
        }

        [ClaimsAuthorize("Pedido", "Visualizar")]
        [HttpGet("api/lista-pedidos")]
        public async Task<IActionResult> ObterPedidos()
        {
            var pedidos = await _queries.ObterListaPedidos();
            return pedidos == null ? NotFound() : CustomResponse(pedidos);
        }

        [ClaimsAuthorize("Pedido", "Visualizar")]
        [HttpGet("api/pedido")]
        public async Task<IActionResult> ObterPedidoPorId(Guid pedidoId)
        {
            var pedido = await _queries.ObterPedidoPorId(pedidoId);

            return pedido == null ? NotFound() : CustomResponse(pedido);
        }

        [ClaimsAuthorize("Pedido", "Adicionar")]
        [HttpPost("api/pedido")]
        public async Task<IActionResult> AdicionarPedido([FromBody] PedidoViewModel item)
        {
            var command = new AdicionarPedidoCommand(item.ClienteId);
            return CustomResponse(await _mediatorHandler.Send(command));
        }

        [ClaimsAuthorize("Pedido", "Emitir")]
        [HttpPut("api/emitir-pedido")]
        public async Task<IActionResult> EmitirPedido(Guid pedidoId)
        {
            var command = new EmitirPedidoCommand(pedidoId);
            return CustomResponse(await _mediatorHandler.Send(command));
        }

        [ClaimsAuthorize("Pedido", "Autorizar")]
        [HttpPut("api/autorizar-pedido")]
        public async Task<IActionResult> AutorizarPedido(Guid pedidoId)
        {
            var command = new AutorizarPedidoCommand(pedidoId);
            return CustomResponse(await _mediatorHandler.Send(command));
        }

        [ClaimsAuthorize("Pedido", "Despachar")]
        [HttpPut("api/despachar-pedido")]
        public async Task<IActionResult> DespacharPedido(Guid pedidoId)
        {
            var command = new DespacharPedidoCommand(pedidoId);
            return CustomResponse(await _mediatorHandler.Send(command));
        }

        [ClaimsAuthorize("Pedido", "Entregar")]
        [HttpPut("api/entregar-pedido")]
        public async Task<IActionResult> EntregarPedido(Guid pedidoId)
        {
            var command = new EntregarPedidoCommand(pedidoId);
            return CustomResponse(await _mediatorHandler.Send(command));
        }

        [ClaimsAuthorize("Pedido", "Cancelar")]
        [HttpPut("api/cancelar-pedido")]
        public async Task<IActionResult> CancelarPedido(Guid pedidoId)
        {
            var command = new CancelarPedidoCommand(pedidoId);
            return CustomResponse(await _mediatorHandler.Send(command));
        }

    }
}

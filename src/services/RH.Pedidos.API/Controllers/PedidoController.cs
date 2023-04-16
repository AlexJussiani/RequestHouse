using MediatR;
using Microsoft.AspNetCore.Mvc;
using RH.Core.Controllers;
using RH.Pedidos.API.Application.Commands;
using RH.Pedidos.API.Application.Queries;
using RH.Pedidos.API.Application.Queries.ViewModels;
using RH.Pedidos.API.Services;
using System;
using System.Threading.Tasks;

namespace RH.Pedidos.API.Controllers
{
    public class PedidoController : MainController
    {
       private readonly IMediator _mediatorHandler;
        private readonly IPedidoQueries _queries;

        public PedidoController(IMediator mediatorHandler, IPedidoQueries queries)
        {
            _mediatorHandler = mediatorHandler;
            _queries = queries;
        }

        [HttpGet("api/pedido")]
        public async Task<IActionResult> ObterPedidoPorId(Guid pedidoId)
        {
            var pedido = await _queries.ObterPedidoPorId(pedidoId);

            return pedido == null ? NotFound() : CustomResponse(pedido);
        }

        [HttpPost("api/pedido")]
        public async Task<IActionResult> AdicionarPedido([FromBody] PedidoViewModel item)
        {
            var command = new AdicionarPedidoCommand(item.ClienteId);
            return CustomResponse(await _mediatorHandler.Send(command));
        }

        [HttpPost("api/pedidoItem")]
        public async Task<IActionResult> AdicionarItemPedido([FromBody] ItemViewModel item)
        {
            var command = new AdicionarItemPedidoCommand(item.PedidoId, item.ProdutoId, item.ProdutoNome, item.Quantidade, item.ValorUnitario);
            return CustomResponse(await _mediatorHandler.Send(command));             
        }

        [HttpPut("api/pedidoItem")]
        public async Task<IActionResult> AtualizarItemPedido([FromBody] ItemViewModel item)
        {
            var command = new AtualizarItemPedidoCommand(item.PedidoId, item.ProdutoId, item.Quantidade);
            return CustomResponse(await _mediatorHandler.Send(command));
        }

        [HttpDelete("api/pedidoItem")]
        public async Task<IActionResult> AtualizarItemPedido(Guid pedidoId, Guid produtoId)
        {
            var command = new RemoverItemPedidoCommand(pedidoId, produtoId);
            return CustomResponse(await _mediatorHandler.Send(command));
        }
    }
}

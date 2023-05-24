using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RH.Core.Controllers;
using RH.Core.Identidade;
using RH.Core.Usuario;
using RH.Pedidos.API.Application.Commands;
using RH.Pedidos.API.Application.Queries;
using RH.Pedidos.API.Application.Queries.ViewModels;
using RH.Pedidos.API.Services;
using RH.Pedidos.Domain;
using System;
using System.Threading.Tasks;

namespace RH.Pedidos.API.Controllers
{
    [Authorize]
    public class PedidoItemController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly IMediator _mediatorHandler;
        private readonly IPedidoQueries _queries;

        public PedidoItemController(IMediator mediatorHandler, IPedidoQueries queries, IAspNetUser user)
        {
            _mediatorHandler = mediatorHandler;
            _queries = queries;
            _user = user;
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
        [HttpPost("api/pedidoItem")]
        public async Task<IActionResult> AdicionarItemPedido([FromBody] ItemViewModel item)
        {
            var command = new AdicionarItemPedidoCommand(item.PedidoId, item.ProdutoId, item.ProdutoNome, item.Quantidade, item.ValorUnitario);
            return CustomResponse(await _mediatorHandler.Send(command));             
        }

        [ClaimsAuthorize("Pedido", "Atualizar")]
        [HttpPut("api/pedidoItem")]
        public async Task<IActionResult> AtualizarItemPedido([FromBody] ItemViewModel item)
        {
            var command = new AtualizarItemPedidoCommand(item.PedidoId, item.ProdutoId, item.Quantidade);
            return CustomResponse(await _mediatorHandler.Send(command));
        }

        [ClaimsAuthorize("Produto", "Excluir")]
        [HttpDelete("api/pedidoItem")]
        public async Task<IActionResult> AtualizarItemPedido(Guid pedidoId, Guid produtoId)
        {
            var command = new RemoverItemPedidoCommand(pedidoId, produtoId);
            return CustomResponse(await _mediatorHandler.Send(command));
        }
    }
}

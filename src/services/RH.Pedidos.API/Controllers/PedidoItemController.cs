using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RH.Core.Controllers;
using RH.Core.Identidade;
using RH.Core.Usuario;
using RH.Pedidos.API.Application.Commands;
using RH.Pedidos.API.Application.Queries;
using RH.Pedidos.API.Application.Queries.ViewModels;
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

        [ClaimsAuthorize("Pedido", "Adicionar")]
        [HttpPost("api/pedidoItem")]
        public async Task<IActionResult> AdicionarItemPedido([FromBody] ItemViewModel item)
        {
            var command = new AdicionarItemPedidoCommand(item.PedidoId, item.ProdutoId, item.ProdutoNome, item.Quantidade, item.ValorUnitario);
            return CustomResponse(await _mediatorHandler.Send(command));             
        }

        [ClaimsAuthorize("Pedido", "Atualizar")]
        [HttpPut("api/pedidoItem")]
        public async Task<IActionResult> AtualizarItemPedido(Guid pedidoId, Guid produtoId, int quantidade)
        {
            var command = new AtualizarItemPedidoCommand(pedidoId, produtoId, quantidade);
            return CustomResponse(await _mediatorHandler.Send(command));
        }

        [ClaimsAuthorize("Pedido", "Excluir")]
        [HttpDelete("api/pedidoItem")]
        public async Task<IActionResult> AtualizarItemPedido(Guid pedidoId, Guid produtoId)
        {
            var command = new RemoverItemPedidoCommand(pedidoId, produtoId);
            return CustomResponse(await _mediatorHandler.Send(command));
        }
    }
}

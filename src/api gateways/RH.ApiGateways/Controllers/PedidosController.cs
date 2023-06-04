using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RH.ApiGateways.Services;
using System;
using RH.Core.Controllers;
using System.Threading.Tasks;
using RH.Core.Identidade;

namespace RH.ApiGateways.Controllers
{
    [Authorize]
    [Route("api-gateway/pedidos")]
    public class PedidosController : MainController
    {
        private readonly IPedidosService _pedidoService;

        public PedidosController(IPedidosService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet("lista-pedidos")]
        public async Task<IActionResult> ListaContas()
        {
            return CustomResponse(await _pedidoService.ObterListaContas());
        }

        [ClaimsAuthorize("Pedido", "Adicionar")]
        [HttpPost("api/adicionar-pedido")]
        public async Task<IActionResult> AdicionarPedido(Guid idCliente)
        {
            return CustomResponse(await _pedidoService.CriarPedido(idCliente));
        }

        [ClaimsAuthorize("Pedido", "Adicionar")]
        [HttpPost("api/adicionar-item-pedido")]
        public async Task<IActionResult> AdicionarItemPedido(Guid idPedido, Guid idProduto, int quantidade)
        {
            return CustomResponse(await _pedidoService.AdicionarItemPedido(idPedido, idProduto, quantidade));
        }
    }
}

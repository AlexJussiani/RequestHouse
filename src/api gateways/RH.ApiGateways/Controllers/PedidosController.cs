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
    [Route("api-gateway")]
    public class PedidosController : MainController
    {
        private readonly IPedidosService _pedidoService;

        public PedidosController(IPedidosService pedidoService)
        {
            _pedidoService = pedidoService;
        }
        [HttpGet("obter-Pedido-PorId")]
        public async Task<IActionResult> ObterPedidoPorId(Guid pedidoId)
        {
            return CustomResponse(await _pedidoService.ObterPedidoPorId(pedidoId));
        }

        [HttpGet("lista-pedidos")]
        public async Task<IActionResult> ListaPedidos()
        {
            return CustomResponse(await _pedidoService.ObterListaPedidos());
        }

        [HttpGet("lista-pedidos-nao-concluido")]
        public async Task<IActionResult> ListaPedidosNaoConcluido()
        {
            return CustomResponse(await _pedidoService.ObterListaPedidosNaoConcluido());
        }

        [HttpGet("lista-pedidos-concluido")]
        public async Task<IActionResult> ListaPedidosConcluido()
        {
            return CustomResponse(await _pedidoService.ObterListaPedidosConcluido());
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

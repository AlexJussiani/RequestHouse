using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RH.ApiGateways.Services;
using System;
using RH.Core.Controllers;
using System.Threading.Tasks;

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
    }
}

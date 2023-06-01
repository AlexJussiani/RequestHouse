using Microsoft.Extensions.Options;
using RH.ApiGateways.Configurations;
using RH.ApiGateways.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RH.ApiGateways.Services
{
    public interface IPedidosService
    {
        Task<IEnumerable<PedidosDTO>> ObterListaContas();
        Task<PedidosDTO> ObterPedidoPorId(Guid id);
    }
    public class PedidosServices : Service, IPedidosService
    {
        private readonly HttpClient _httpClient;
        private readonly IClientesService _clienteService;

        public PedidosServices(HttpClient httpClient, IOptions<AppServicesSettings> settings, IClientesService clienteService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.PedidoUrl);
            _clienteService = clienteService;
        }

        public async Task<IEnumerable<PedidosDTO>> ObterListaContas()
        {
            var response = await _httpClient.GetAsync("lista-pedidos");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            var pedidos = await DeserializarObjetoResponse<IEnumerable<PedidosDTO>>(response);

            foreach(var pedido in pedidos)
            {
                var cliente = await _clienteService.ObterClientePorId(pedido.ClienteId);
                pedido.ClienteNome = cliente.Nome;
            }

            return pedidos;
        }

        public Task<PedidosDTO> ObterPedidoPorId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Threading.Tasks;
using System;
using RH.ApiGateways.Models;
using Microsoft.Extensions.Options;
using RH.ApiGateways.Configurations;
using System.Net.Http;
using System.Net;

namespace RH.ApiGateways.Services
{
    public interface IClientesService
    {
        Task<ClienteDTO> ObterClientePorId(Guid id);
    }
    public class ClientesServices : Service, IClientesService
    {
        private readonly HttpClient _httpClient;

        public ClientesServices(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ClienteUrl);
        }
        public async Task<ClienteDTO> ObterClientePorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"cliente-obterPorId?id={id}");
            if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.NoContent) return null;

            TratarErrosResponse(response);

            var cliente = await DeserializarObjetoResponse<ClienteDTO>(response);

            return cliente;
        }
    }
}

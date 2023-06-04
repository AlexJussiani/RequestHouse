using System.Threading.Tasks;
using System;
using RH.ApiGateways.Models;
using Microsoft.Extensions.Options;
using RH.ApiGateways.Configurations;
using System.Net.Http;
using System.Net;

namespace RH.ApiGateways.Services
{
    public interface IProdutosServices
    {
        Task<ProdutoDTO> ObterProdutoPorId(Guid id);
    }
    public class ProdutosServices : Service, IProdutosServices
    {
        private readonly HttpClient _httpClient;

        public ProdutosServices(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ProdutoUrl);
        }
        public async Task<ProdutoDTO> ObterProdutoPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"produto-obterPorId?produtoId={id}");
            if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.NoContent) return null;

            TratarErrosResponse(response);

            var cliente = await DeserializarObjetoResponse<ProdutoDTO>(response);

            return cliente;
        }
    }
}

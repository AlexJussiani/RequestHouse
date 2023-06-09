using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RH.ApiGateways.Configurations;
using RH.ApiGateways.Models;
using RH.Core.Communication;
using RH.Core.Messages;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RH.ApiGateways.Services
{
    public interface IPedidosService
    {
        Task<IEnumerable<PedidosDTO>> ObterListaPedidosNaoConcluido();
        Task<IEnumerable<PedidosDTO>> ObterListaPedidosConcluido();
        Task<IEnumerable<PedidosDTO>> ObterListaPedidos();
        Task<PedidosDTO> ObterPedidoPorId(Guid id);
        Task<ResponseResult> CriarPedido(Guid idCliente);
        Task<ResponseResult> AdicionarItemPedido(Guid idPedido, Guid idProduto, int quantidade);
    }
    public class PedidosServices : Service, IPedidosService
    {
        private readonly HttpClient _httpClient;
        private readonly IClientesService _clienteService;
        private readonly IProdutosServices _produtoService;

        public PedidosServices(HttpClient httpClient, IOptions<AppServicesSettings> settings, IClientesService clienteService, IProdutosServices produtoService)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.PedidoUrl);
            _clienteService = clienteService;
            _produtoService = produtoService;
        }

        public async Task<IEnumerable<PedidosDTO>> ObterListaPedidosNaoConcluido()
        {
            var response = await _httpClient.GetAsync("lista-pedidos-nao-concluidos");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            var pedidos = await DeserializarObjetoResponse<IEnumerable<PedidosDTO>>(response);

            foreach (var pedido in pedidos)
            {
                var cliente = await _clienteService.ObterClientePorId(pedido.ClienteId);
                pedido.ClienteNome = cliente.Nome;
            }

            return pedidos;
        }

        public async Task<IEnumerable<PedidosDTO>> ObterListaPedidosConcluido()
        {
            var response = await _httpClient.GetAsync("lista-pedidos-concluidos");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            var pedidos = await DeserializarObjetoResponse<IEnumerable<PedidosDTO>>(response);

            foreach (var pedido in pedidos)
            {
                var cliente = await _clienteService.ObterClientePorId(pedido.ClienteId);
                pedido.ClienteNome = cliente.Nome;
            }

            return pedidos;
        }

        public async Task<IEnumerable<PedidosDTO>> ObterListaPedidos()
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

        public async Task<PedidosDTO> ObterPedidoPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"pedido?pedidoId={id}");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;

            TratarErrosResponse(response);

            var pedido = await DeserializarObjetoResponse<PedidosDTO>(response);
            var cliente = await _clienteService.ObterClientePorId(pedido.ClienteId);
            pedido.ClienteNome = cliente.Nome;
            return pedido;
        }

        public async Task<ResponseResult> CriarPedido(Guid idCliente)
        {
            dynamic clienteId = new ExpandoObject();
            clienteId.ClienteId = idCliente;
            var cliente = await _clienteService.ObterClientePorId(idCliente);

            if(cliente == null)
            {
                AdicionarErro("Cliente Não localizado");
                return RetornoValidation();
            }
            var itemContent = ObterConteudo(clienteId);

            var response = await _httpClient.PostAsync("pedido/", itemContent);
            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ValidationResult>(response);
            return RetornoOk(); 
        }

        public async Task<ResponseResult> AdicionarItemPedido(Guid idPedido, Guid idProduto, int quantidade)
        {            
            var produto = await _produtoService.ObterProdutoPorId(idProduto);

            if (produto == null)
            {
                AdicionarErro("Produto Não localizado");
                return RetornoValidation();
            }

            //Mapear item pedido
            var itemPedido = new ItemPedidoDTO
            {
                ProdutoId = idProduto,
                PedidoId = idPedido,
                ProdutoNome = produto.Nome,
                Quantidade = quantidade,
                ValorUnitario = produto.Valor
            };   
            var itemContent = ObterConteudo(itemPedido);

            var response = await _httpClient.PostAsync("pedidoItem/", itemContent);
            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);
            return RetornoOk();
        }       
    }
}

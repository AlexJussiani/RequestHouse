using Microsoft.AspNetCore.Mvc;
using RH.Clientes.API.Application.DTO;
using RH.Clientes.API.Data.Repository;
using RH.Clientes.API.Services;
using RH.Core.Controllers;
using System;
using System.Threading.Tasks;

namespace RH.Clientes.API.Controllers
{
    [Route("api/clientes")]
    public class ClienteController : MainController
    {
        private readonly IClienteRepository _repository;
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteRepository repository, IClienteService clienteService)
        {
            _repository = repository;
            _clienteService = clienteService;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _repository.ObterTodosClientes());
        }

        [HttpGet("api/cliente-obterPorId")]
        public async Task<IActionResult> ObterPedidoPorId(Guid id)
        {
            return CustomResponse(await _repository.ObterClientePorId(id));
        }

        [HttpPost()]
        public async Task<ActionResult> Registrar(ClienteDTO cliente)
        {
            return CustomResponse(await _clienteService.AdicionarCliente(cliente));
        }

        [HttpPut()]
        public async Task<ActionResult> Atualizar(Guid idCliente, ClienteDTO cliente)
        {
            return CustomResponse(await _clienteService.AtualizarCliente(idCliente, cliente));
        }

        [HttpDelete()]
        public async Task<ActionResult> Remover(Guid idCliente)
        {
            return CustomResponse(await _clienteService.RemoverCliente(idCliente));
        }
    }
}

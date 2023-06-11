using Microsoft.AspNetCore.Mvc;
using RH.Clientes.API.Data.Repository;
using RH.Clientes.API.Models;
using RH.Clientes.API.Services;
using RH.Core.Controllers;
using System;
using System.Threading.Tasks;

namespace RH.Clientes.API.Controllers
{
    [Route("api/clientes")]
    public class ContatoClienteController : MainController
    {
        private readonly IClienteRepository _repository;
        private readonly IClienteService _clienteService;

        public ContatoClienteController(IClienteRepository repository, IClienteService clienteService)
        {
            _repository = repository;
            _clienteService = clienteService;
        }

        [HttpGet("api/contato-cliente")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse(await _repository.ObterTodosClientes());
        }

        [HttpGet("api/contato-cliente-obterPorId")]
        public async Task<IActionResult> ObterPedidoPorId(Guid id)
        {
            return CustomResponse(await _repository.ObterContatoClientePorId(id));
        }

        [HttpPost("api/contato/cliente")]
        public async Task<ActionResult> Registrar(ContatoCliente contato)
        {
            return CustomResponse(await _clienteService.AdicionarContatoCliente(contato));
        }

        [HttpPut("api/contato/cliente")]
        public async Task<ActionResult> Atualizar(Guid idCliente, ContatoCliente contato)
        {
            return CustomResponse(await _clienteService.AtualizarContatoCliente(idCliente, contato));
        }

        [HttpDelete("api/contato/cliente")]
        public async Task<ActionResult> Remover(Guid idContato)
        {
            return CustomResponse(await _clienteService.RemoverContatoCliente(idContato));
        }
    }
}

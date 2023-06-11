using System.Threading.Tasks;
using System;
using FluentValidation.Results;
using RH.Clientes.API.Application.DTO;
using RH.Clientes.API.Models;

namespace RH.Clientes.API.Services
{
    public interface IClienteService
    {
        //Cliente
        Task<ValidationResult> AdicionarCliente(ClienteDTO cliente);
        Task<ValidationResult> AtualizarCliente(Guid idCliente, ClienteDTO cliente);
        Task<ValidationResult> RemoverCliente(Guid idCliente);

        //Contato
        Task<ValidationResult> AdicionarContatoCliente(ContatoCliente contato);
        Task<ValidationResult> AtualizarContatoCliente(Guid idContato, ContatoCliente contato);
       Task<ValidationResult> RemoverContatoCliente(Guid idContato);
    }
}

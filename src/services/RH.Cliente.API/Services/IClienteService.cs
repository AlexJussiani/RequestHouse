using System.Threading.Tasks;
using System;
using FluentValidation.Results;
using RH.Clientes.API.Application.DTO;

namespace RH.Clientes.API.Services
{
    public interface IClienteService
    {
        Task<ValidationResult> Adicionar(ClienteDTO produto);
        Task<ValidationResult> Atualizar(Guid idCliente, ClienteDTO cliente);
        Task<ValidationResult> Remover(Guid idCliente);
    }
}

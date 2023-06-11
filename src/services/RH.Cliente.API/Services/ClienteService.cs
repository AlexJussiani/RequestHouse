using AutoMapper;
using FluentValidation.Results;
using RH.Clientes.API.Application.DTO;
using RH.Clientes.API.Data.Repository;
using RH.Clientes.API.Models;
using RH.Core.Messages;
using System;
using System.Threading.Tasks;

namespace RH.Clientes.API.Services
{
    public class ClienteService : CommandHandler, IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ValidationResult> AdicionarCliente(ClienteDTO clienteDTO)
        {
            _repository.AdicionarCliente(_mapper.Map<Cliente>(clienteDTO));
            return await PersistirDados(_repository.UnitOfWork);
        }

        public async Task<ValidationResult> AdicionarContatoCliente(ContatoCliente contato)
        {
            _repository.AdicionarContatoCliente(contato);
            return await PersistirDados(_repository.UnitOfWork);
        }

        public async Task<ValidationResult> AtualizarCliente(Guid idCliente, ClienteDTO clienteDTO)
        {
            var cliente = await _repository.ObterClientePorId(idCliente);

            if (cliente == null)
            {
                AdicionarErro("Cliente não localizado");
                return ValidationResult;
            }

            cliente = AlterarCliente(cliente, clienteDTO);
            _repository.AtualizarCliente(cliente);
            return await PersistirDados(_repository.UnitOfWork);
        }

        public Task<ValidationResult> AtualizarContatoCliente(Guid idCliente, ContatoCliente cliente)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> RemoverCliente(Guid idCliente)
        {
            var cliente = await _repository.ObterClientePorId(idCliente);

            if (cliente == null)
                return ValidationResult;

            _repository.RemoverCliente(cliente);
            return await PersistirDados(_repository.UnitOfWork);
        }

        public async Task<ValidationResult> RemoverContatoCliente(Guid idContato)
        {
            var contato = await _repository.ObterContatoClientePorId(idContato);

            if (contato == null)
                return ValidationResult;

            _repository.RemoverContatoCliente(contato);
            return await PersistirDados(_repository.UnitOfWork);
        }

        private Cliente AlterarCliente(Cliente cliente, ClienteDTO clienteDTO)
        {
            cliente.Cpf.Numero = clienteDTO.Cpf;
            cliente.EhCliente = clienteDTO.EhCliente;
            cliente.EhFornecedor = clienteDTO.EhFornecedor;
            cliente.Email.Endereco = clienteDTO.Email;
            cliente.Nome = clienteDTO.Nome;
            cliente.Telefone = clienteDTO.Telefone;
            return cliente;
        }
    }
}

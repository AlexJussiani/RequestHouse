using RH.Clientes.API.Models;
using RH.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Clientes.API.Data.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        //Cliente
        Task<IEnumerable<Cliente>> ObterTodosClientes();
        Task<Cliente> ObterClientePorId(Guid id);
        Task<IEnumerable<Cliente>> ObterClientePorName(string name);
        void AdicionarCliente(Cliente cliente);
        void AtualizarCliente(Cliente cliente);
        void RemoverCliente(Cliente cliente);

        //Contato
        Task<IEnumerable<ContatoCliente>> ObterTodosContatosClientes();
        Task<ContatoCliente> ObterContatoClientePorId(Guid id);
        void AdicionarContatoCliente(ContatoCliente contato);
        void AtualizarContatoCliente(ContatoCliente contato);
        void RemoverContatoCliente(ContatoCliente contato);
    }
}

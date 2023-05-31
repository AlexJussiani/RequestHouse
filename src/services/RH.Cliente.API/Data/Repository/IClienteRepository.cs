using RH.Clientes.API.Models;
using RH.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Clientes.API.Data.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> ObterTodos();
        Task<Cliente> ObterPorId(Guid id);
        Task<IEnumerable<Cliente>> ObterPorName(string name);
        void Adicionar(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Remover(Cliente cliente);
    }
}

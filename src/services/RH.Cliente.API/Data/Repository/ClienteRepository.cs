using Microsoft.EntityFrameworkCore;
using RH.Clientes.API.Models;
using RH.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RH.Clientes.API.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClientesContext _context;

        public ClienteRepository(ClientesContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Cliente cliente)
        {
            _context.Add(cliente);
        }

        public void Atualizar(Cliente cliente)
        {
            _context.Update(cliente);
        }

        public async Task<Cliente> ObterPorId(Guid id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<IEnumerable<Cliente>> ObterPorName(string name)
        {
            return await _context.Clientes.AsNoTracking().Where(c => c.Nome.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObterTodos()
        {
            return await _context.Clientes.AsNoTracking().ToListAsync();
        }

        public void Remover(Cliente cliente)
        {
            _context.Remove(cliente);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}

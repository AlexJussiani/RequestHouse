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

        public void AdicionarCliente(Cliente cliente)
        {
            _context.Add(cliente);
        }

        public void AtualizarCliente(Cliente cliente)
        {
            _context.Update(cliente);
        }

        public async Task<Cliente> ObterClientePorId(Guid id)
        {
            return await _context.Clientes.Include(c => c.Contatos).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cliente>> ObterClientePorName(string name)
        {
            return await _context.Clientes.AsNoTracking().Where(c => c.Nome.Contains(name)).ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObterTodosClientes()
        {
            return await _context.Clientes.AsNoTracking().OrderBy(c => c.Nome).ToListAsync();
        }

        public void RemoverCliente(Cliente cliente)
        {
            _context.Remove(cliente);
        }      

        public async Task<IEnumerable<ContatoCliente>> ObterTodosContatosClientes()
        {
            return await _context.Contatos.AsNoTracking().OrderBy(c => c.Nome).ToListAsync();
        }

        public async Task<ContatoCliente> ObterContatoClientePorId(Guid id)
        {
            return await _context.Contatos.FindAsync(id);
        }

        public void AdicionarContatoCliente(ContatoCliente contato)
        {
            _context.Add(contato);
        }

        public void AtualizarContatoCliente(ContatoCliente contato)
        {
            _context.Update(contato);
        }

        public void RemoverContatoCliente(ContatoCliente contato)
        {
            _context.Remove(contato);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RH.Core.Data;
using RH.Produtos.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RH.Produtos.API.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutosContext _context;

        public ProdutoRepository(ProdutosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Produto produto)
        {
            _context.Add(produto);
        }

        public void Atualizar(Produto produto)
        {
            _context.Update(produto);
        }

        public async Task<Produto> ObterPorId(Guid id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task<Produto> ObterPorName(string name)
        {
            return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(c => c.Nome.Equals(name));
        }

        public async Task<IEnumerable<Produto>> ObterProdutosEntrada()
        {
            return await _context.Produtos.AsNoTracking().Where(c => c.Ativo == true && c.Entrada == true).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosSaida()
        {
            return await _context.Produtos.AsNoTracking().Where(c => c.Ativo == true && c.Saida == true).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _context.Produtos.AsNoTracking().Where(c => c.Ativo == true).ToListAsync();
        }

        public void Remover(Produto produto)
        {
            _context.Remove(produto);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

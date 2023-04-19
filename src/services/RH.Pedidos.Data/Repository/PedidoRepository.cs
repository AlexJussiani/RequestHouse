using Microsoft.EntityFrameworkCore;
using RH.Core.Data;
using RH.Pedidos.Domain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH.Pedidos.Data.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly PedidosContext _context;

        public PedidoRepository(PedidosContext context)
        {
            _context = context;
        }

        public DbConnection ObterConexao() => _context.Database.GetDbConnection();

        public IUnitOfWork UnitOfWork => _context;

        public Task<PedidoItem> ObterItemPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Pedido> ObterPorId(Guid id)
        {
            return await _context.Pedidos
                .Include(p => p.PedidoItems)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            
        }

        public async Task<IEnumerable<Pedido>> ObterListaPedidosRascunho()
        {
            return await _context.Pedidos.Where(p => p.PedidoStatus == PedidoStatus.Rascunho).ToListAsync();
        }

        public async Task<Pedido> ObterPorPedidoId(Guid pedidoId)
        {
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == pedidoId);
            if (pedido == null) return null;

            await _context.Entry(pedido)
                .Collection(i => i.PedidoItems).LoadAsync();

            return pedido;
        }

        public void Adicionar(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
        }

        public void Atualizar(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            _context.PedidoItems.Add(pedidoItem);
        }

        public void AtualizarItem(PedidoItem pedidoItem)
        {
            _context.PedidoItems.Update(pedidoItem);
        }

        public void RemoverItem(PedidoItem pedidoItem)
        {
            _context.PedidoItems.Remove(pedidoItem);
        }       

        public async Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId)
        {
            return await _context.PedidoItems.FirstOrDefaultAsync(p => p.ProdutoId == produtoId && p.PedidoId == pedidoId);
        }

        public async Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId)
        {
            return await _context.Pedidos.Where(c => c.ClienteId == clienteId).ToListAsync();
        }       

        public void Dispose()
        {
            _context.Dispose();
        }        
    }
}

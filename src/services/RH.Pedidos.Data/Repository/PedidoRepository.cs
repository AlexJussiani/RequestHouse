using Microsoft.EntityFrameworkCore;
using RH.Core.Data;
using RH.Pedidos.Domain;
using System;
using System.Collections.Generic;
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
        public IUnitOfWork UnitOfWork => _context;

        public async Task<Pedido> ObterPedidoRascunhoPorPedidoId(Guid pedidoId)
        {
            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == pedidoId && p.PedidoStatus == PedidoStatus.Rascunho);
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

        public Task<PedidoItem> ObterItemPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId)
        {
            return await _context.PedidoItems.FirstOrDefaultAsync(p => p.ProdutoId == produtoId && p.PedidoId == pedidoId);
        }

        public Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

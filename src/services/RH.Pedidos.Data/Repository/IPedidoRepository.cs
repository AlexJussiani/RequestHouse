using RH.Core.Data;
using RH.Pedidos.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Data.Common;

namespace RH.Pedidos.Data.Repository
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<Pedido> ObterPorId(Guid id);
        Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId);
        Task<IEnumerable<Pedido>> ObterListaPedidosRascunho();
        Task<Pedido> ObterPedidoRascunhoPorPedidoId(Guid pedidoId);
        void Adicionar(Pedido pedido);
        void Atualizar(Pedido pedido);

        DbConnection ObterConexao();

        Task<PedidoItem> ObterItemPorId(Guid id);
        Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId);
        void AdicionarItem(PedidoItem pedidoItem);
        void AtualizarItem(PedidoItem pedidoItem);
        void RemoverItem(PedidoItem pedidoItem);
    }
}

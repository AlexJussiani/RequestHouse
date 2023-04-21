using RH.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RH.Pedidos.Domain
{  
  public class Pedido : Entity, IAggregateRoot
    {
        public int Codigo { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime? DataAutorizacao { get; private set; }
        public DateTime? DataConclusao { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }

        private readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;
        public Endereco EnderecoEntrega { get; private set; }

        protected Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }

        public void AdicionarItem(PedidoItem item)
        {
            item.AssociarPedido(Id);
            if (ItemExistente(item))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(i => i.ProdutoId == item.ProdutoId);
                itemExistente.AdicionarUnidade(item.Quantidade);
                item = itemExistente;

                _pedidoItems.Remove(itemExistente);
            }            
            _pedidoItems.Add(item);
            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem item)
        {
            ValidarPedidoItemInexistente(item);
            item.AssociarPedido(Id);

            var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

            _pedidoItems.Remove(itemExistente);
            _pedidoItems.Add(item);

            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem item)
        {
            ValidarPedidoItemInexistente(item);
            _pedidoItems.Remove(item);
            CalcularValorPedido();
        }

        public void AtualizarUnidades(PedidoItem item, int unidades)
        {
            item.AtualizarUnidades(unidades);
            AtualizarItem(item);
        }

        private void ValidarPedidoItemInexistente(PedidoItem item)
        {
            if (!ItemExistente(item)) throw new DomainException("O item não pertence ao pedido");
        }

        public bool ItemExistente(PedidoItem item)
        {
            return _pedidoItems.Any(i => i.ProdutoId == item.ProdutoId);
        }

        private void CalcularValorPedido()
        {
            ValorTotal = _pedidoItems.Sum(i => i.Quantidade * i.ValorUnitario);
        }

        private void TornarRascunho()
        {
            PedidoStatus = PedidoStatus.Rascunho;
        }

        public void EmitirPedido()
        {
            PedidoStatus = PedidoStatus.Emitido;
        }

        public void AutorizarPedido()
        {
            PedidoStatus = PedidoStatus.Autorizado;
            DataAutorizacao = DateTime.Now;
        }
        public void DespacharPedido()
        {
            PedidoStatus = PedidoStatus.Percurso;
        }

        public void EntregarPedido()
        {
            PedidoStatus = PedidoStatus.Entregue;
            DataConclusao = DateTime.Now;
        }

        public void CancelarPedido()
        {
            PedidoStatus = PedidoStatus.Cancelado;
            DataConclusao = DateTime.Now;
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido
                {
                    ClienteId = clienteId,
                };

                pedido.TornarRascunho();
                return pedido;
            }
        }
   }
}

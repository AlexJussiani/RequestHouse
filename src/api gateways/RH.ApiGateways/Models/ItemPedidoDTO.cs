using System;

namespace RH.ApiGateways.Models
{
    public class ItemPedidoDTO
    {
        public Guid Id { get; private set; }
        public Guid ProdutoId { get; set; }
        public Guid PedidoId { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
    }
}

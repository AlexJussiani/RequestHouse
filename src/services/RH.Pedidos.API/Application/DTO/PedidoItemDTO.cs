using System;

namespace RH.Pedidos.API.Application.DTO
{
    public class PedidoItemDTO
    {
        public Guid idItem { get; set; }
        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }
    }
}

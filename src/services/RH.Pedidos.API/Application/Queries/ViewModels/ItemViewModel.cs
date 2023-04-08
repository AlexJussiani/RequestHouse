using System;

namespace RH.Pedidos.API.Application.Queries.ViewModels
{
    public class ItemViewModel
    {
        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public Guid PedidoId { get; set; }
        public string ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
    }
}

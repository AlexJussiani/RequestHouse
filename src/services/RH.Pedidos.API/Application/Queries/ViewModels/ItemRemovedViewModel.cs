using System;

namespace RH.Pedidos.API.Application.Queries.ViewModels
{
    public class ItemRemovedViewModel
    {
        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
    }
}

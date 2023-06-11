using System;

namespace RH.Pedidos.API.Application.Queries.ViewModels
{
    public class PedidoValorViewModel
    {
        public Guid PedidoId { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorAcrescimo { get; set; }
        public string Observacoes { get; set; }
    }
}

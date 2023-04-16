using System;
using System.Collections.Generic;

namespace RH.Pedidos.API.Application.DTO
{
    public class PedidoDTO
    {
        public Guid IdPedido { get; set; }
        public int Codigo { get; set; }

        public Guid ClienteId { get; set; }
        public int PedidoStatus { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAutorizacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public decimal ValorTotal { get; set; }
        public List<PedidoItemDTO> PedidoItems { get; set; }
    }
}

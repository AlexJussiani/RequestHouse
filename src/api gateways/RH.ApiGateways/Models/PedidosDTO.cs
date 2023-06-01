using System;
using System.Collections.Generic;

namespace RH.ApiGateways.Models
{
    public class PedidosDTO
    {        
        public Guid IdPedido { get;  set; }
        public Guid ClienteId { get; set; }
        public string ClienteNome { get;  set; }
        public int Codigo { get;  set; }
        public decimal ValorTotal { get;  set; }
        public DateTime DataCadastro { get;  set; }
        public DateTime? DataAutorizacao { get;  set; }
        public DateTime? DataConclusao { get;  set; }
        public int PedidoStatus { get;  set; }
        public List<ItemPedidoDTO> PedidoItems { get;  set; }
    }
}

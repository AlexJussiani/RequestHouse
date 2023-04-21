using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH.Core.Messages.Integration
{
    public class PedidoAutorizadoIntegrationEvent : IntegrationEvent
    {
        public int CodigoPedido { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal Valor { get; private set; }

        public PedidoAutorizadoIntegrationEvent(int codigoPedido, Guid pedidoId, Guid clienteId, decimal valor)
        {
            CodigoPedido = codigoPedido;
            PedidoId = pedidoId;
            ClienteId = clienteId;
            Valor = valor;
        }
    }
}

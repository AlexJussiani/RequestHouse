using RH.Core.Messages;
using System;

namespace RH.Pedidos.API.Application.Events
{
    public class PedidoAutorizadoEvent : Event
    {
        public int CodigoPedido { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal ValorPedido { get; private set; }

        public PedidoAutorizadoEvent(int codigoPedido, Guid pedidoId, Guid clienteId, decimal valorPedido)
        {
            CodigoPedido = codigoPedido;
            PedidoId = pedidoId;
            ClienteId = clienteId;
            ValorPedido = valorPedido;
        }
    }
}

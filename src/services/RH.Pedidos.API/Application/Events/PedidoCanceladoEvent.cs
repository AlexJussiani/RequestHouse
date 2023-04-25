using RH.Core.Messages;
using System;

namespace RH.Pedidos.API.Application.Events
{
    public class PedidoCanceladoEvent : Event
    { 
        public Guid PedidoId { get; private set; }

        public PedidoCanceladoEvent(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }
    }
}

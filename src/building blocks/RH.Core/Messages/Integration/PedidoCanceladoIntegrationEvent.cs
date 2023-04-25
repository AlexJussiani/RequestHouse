using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH.Core.Messages.Integration
{
    public class PedidoCanceladoIntegrationEvent : IntegrationEvent
    {
        public Guid PedidoId { get; private set; }

        public PedidoCanceladoIntegrationEvent(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }
    }
}

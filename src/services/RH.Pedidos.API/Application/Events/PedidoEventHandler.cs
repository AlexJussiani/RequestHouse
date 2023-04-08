using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Pedidos.API.Application.Events
{
    public class PedidoEventHandler : INotificationHandler<PedidoItemAdicionadoEvent>
    {
        public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

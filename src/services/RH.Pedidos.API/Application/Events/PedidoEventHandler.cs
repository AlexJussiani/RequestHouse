using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Pedidos.API.Application.Events
{
    public class PedidoEventHandler : 
        INotificationHandler<PedidoAdicionadoEvent>,
        INotificationHandler<PedidoItemAdicionadoEvent>,
        INotificationHandler<PedidoItemAtualizadoEvent>,
        INotificationHandler<PedidoItemExcluidoEvent>
    {
        public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoItemAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoItemExcluidoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

using MediatR;
using RH.Core.Messages.Integration;
using RH.MessageBus;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Pedidos.API.Application.Events
{    
    public class PedidoEventHandler : 
        INotificationHandler<PedidoAdicionadoEvent>,
        INotificationHandler<PedidoItemAdicionadoEvent>,
        INotificationHandler<PedidoItemAtualizadoEvent>,
        INotificationHandler<PedidoItemExcluidoEvent>,
        INotificationHandler<PedidoAutorizadoEvent>,
        INotificationHandler<PedidoCanceladoEvent>,
        INotificationHandler<PedidoDespachadoEvent>,
        INotificationHandler<PedidoEmitidoEvent>,
        INotificationHandler<PedidoEntregueEvent>
    {
        private readonly IMessageBus _bus;

        public PedidoEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public Task Handle(PedidoItemAdicionadoEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoAdicionadoEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoItemAtualizadoEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoItemExcluidoEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(PedidoAutorizadoEvent message, CancellationToken cancellationToken)
        {
           // return Task.CompletedTask;
            await _bus.PublishAsync(new PedidoAutorizadoIntegrationEvent(message.CodigoPedido, message.PedidoId, message.ClienteId, message.ValorPedido));
        }

        public Task Handle(PedidoCanceladoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoDespachadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoEmitidoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoEntregueEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}

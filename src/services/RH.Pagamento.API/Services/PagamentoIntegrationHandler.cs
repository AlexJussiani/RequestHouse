using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RH.Core.Messages.Integration;
using RH.MessageBus;
using RH.Pagamento.API.Application.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Pagamento.API.Services
{
    public class PagamentoIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public PagamentoIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }

        private void SetSubscribers()
        {
            _bus.SubscribeAsync<PedidoAutorizadoIntegrationEvent>("PedidoAutorizado",
                async request => await ProcessarConta(request));
        }
        private async Task ProcessarConta(PedidoAutorizadoIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var commandHandler = scope.ServiceProvider.GetRequiredService<IMediator>();
                var command = new AdicionarContaCommand(message.CodigoPedido, message.PedidoId, message.ClienteId, message.Valor);
                await commandHandler.Send(command);
            }
        }
    }
}

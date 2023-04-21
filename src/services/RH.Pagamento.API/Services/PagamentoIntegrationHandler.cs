using Microsoft.Extensions.Hosting;
using RH.Core.Messages.Integration;
using RH.MessageBus;
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
            await Task.CompletedTask;
        }
    }
}

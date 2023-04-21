using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RH.Pagamento.API.Services;
using RH.Core.Utils;
using RH.MessageBus;

namespace RH.Pagamento.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<PagamentoIntegrationHandler>();
        }
    }
}

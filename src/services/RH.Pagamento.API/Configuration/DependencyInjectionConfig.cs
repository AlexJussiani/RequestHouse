using Microsoft.Extensions.DependencyInjection;
using RH.Core.Mediator;
using RH.Pagamento.API.Data;

namespace RH.Pagamento.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Data
            services.AddScoped<PagamentoContext>();
        }
    }
}

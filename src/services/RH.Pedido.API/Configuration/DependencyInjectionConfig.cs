using Microsoft.Extensions.DependencyInjection;
using RH.Pedidos.Data.Data;

namespace RH.Pedidos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<PedidosContext>();
        }
    }
}

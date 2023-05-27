using Microsoft.Extensions.DependencyInjection;
using RH.Produtos.API.Data;

namespace RH.Produtos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Data
            services.AddScoped<ProdutosContext>();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using RH.Produtos.API.Data;
using RH.Produtos.API.Data.Repository;
using RH.Produtos.API.Services;
using AutoMapper;

namespace RH.Produtos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Data
            services.AddScoped<ProdutosContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();

            
        }
    }
}

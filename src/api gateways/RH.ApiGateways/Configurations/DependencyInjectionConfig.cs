using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RH.ApiGateways.Services;
using RH.Core.Usuario;

namespace RH.ApiGateways.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IPedidosService, PedidosServices>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IClientesService, ClientesServices>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
        }
    }
}

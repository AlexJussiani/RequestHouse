using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using RH.Core.Usuario;
using RH.Clientes.API.Data;
using RH.Clientes.API.Services;
using RH.Clientes.API.Data.Repository;

namespace RH.Clientes.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteService, ClienteService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<ClientesContext>();
        }
    }
}
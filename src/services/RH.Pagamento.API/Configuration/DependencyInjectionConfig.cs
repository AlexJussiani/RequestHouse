using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RH.Core.Mediator;
using RH.Pagamento.API.Application.Commands;
using RH.Pagamento.API.Data;
using RH.Pagamento.API.Data.Repository;

namespace RH.Pagamento.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Commands
            services.AddScoped<IRequestHandler<AdicionarContaCommand, ValidationResult>, ContaCommandHandler>();

            //Data
            services.AddScoped<PagamentoContext>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
        }
    }
}

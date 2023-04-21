using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RH.Core.Mediator;
using RH.Core.Usuario;
using RH.Pedidos.API.Application.Commands;
using RH.Pedidos.API.Application.Events;
using RH.Pedidos.API.Application.Queries;
using RH.Pedidos.Data;
using RH.Pedidos.Data.Repository;

namespace RH.Pedidos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Commands
            services.AddScoped<IRequestHandler<AdicionarItemPedidoCommand, ValidationResult>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarItemPedidoCommand, ValidationResult>, PedidoCommandHandler>();

            // Events
            services.AddScoped<INotificationHandler<PedidoItemAdicionadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoAdicionadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoAutorizadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoCanceladoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoDespachadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoEmitidoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoEntregueEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoItemAtualizadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoItemExcluidoEvent>, PedidoEventHandler>();


            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IPedidoQueries, PedidoQueries>();

            //Data
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<PedidosContext>();
        }
    }
}

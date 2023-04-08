using MediatR;
using NetDevPack.Messaging;
using RH.Pedidos.API.Application.Commands;
using RH.Pedidos.API.Application.Queries.ViewModels;
using System.Threading.Tasks;

namespace RH.Pedidos.API.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IMediator _mediatorHandler;

        public PedidoService(IMediator mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task<ResponseMessage> AdicionarItemPedido(ItemViewModel item)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResponseMessage> AtualizarItemPedido(ItemViewModel item)
        {
            throw new System.NotImplementedException();
        }

        public Task<ResponseMessage> ExcluirItemPedido(ItemViewModel item)
        {
            throw new System.NotImplementedException();
        }
    }
}

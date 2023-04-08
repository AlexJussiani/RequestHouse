using NetDevPack.Messaging;
using RH.Pedidos.API.Application.Queries.ViewModels;
using System.Threading.Tasks;

namespace RH.Pedidos.API.Services
{
    public interface IPedidoService
    {
        Task<ResponseMessage> AdicionarItemPedido(ItemViewModel item);
        Task<ResponseMessage> AtualizarItemPedido(ItemViewModel item);
        Task<ResponseMessage> ExcluirItemPedido(ItemViewModel item);
    }
}

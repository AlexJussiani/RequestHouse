using RH.Core.Data;
using RH.Pagamento.API.Models;
using System.Threading.Tasks;
using System;

namespace RH.Pagamento.API.Data.Repository
{
    public interface IPagamentoRepository : IRepository<Conta>
    {
        Task<Conta> ObterContaPorIdPedido(Guid idPedido);
        void Adicionar(Conta conta);
    }
}

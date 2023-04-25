using RH.Core.Data;
using RH.Pagamento.API.Models;
using System.Threading.Tasks;
using System;

namespace RH.Pagamento.API.Data.Repository
{
    public interface IPagamentoRepository : IRepository<Conta>
    {
        //Conta
        Task<Conta> ObterContaPorId(Guid id);
        Task<Conta> ObterContaPorIdPedido(Guid idPedido);
        void AdicionarConta(Conta conta);
        void AtualizarConta(Conta conta);

        //Pagamento
        void AdicionarPagamento(PagamentoConta pagamento);
        void RemoverPagamento(PagamentoConta pagamento);
    }
}

using RH.Core.Data;
using RH.Pagamento.API.Models;

namespace RH.Pagamento.API.Data.Repository
{
    public interface IPagamentoRepository : IRepository<Conta>
    {
        void Adicionar(Conta conta);
    }
}

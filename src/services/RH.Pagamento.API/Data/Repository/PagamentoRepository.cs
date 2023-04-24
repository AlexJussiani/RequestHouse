using RH.Core.Data;
using RH.Pagamento.API.Models;

namespace RH.Pagamento.API.Data.Repository
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly PagamentoContext _context;

        public PagamentoRepository(PagamentoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Conta conta)
        {
            _context.Contas.Add(conta);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

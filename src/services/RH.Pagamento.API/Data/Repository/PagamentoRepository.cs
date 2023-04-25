using Microsoft.EntityFrameworkCore;
using RH.Core.Data;
using RH.Pagamento.API.Models;
using System;
using System.Threading.Tasks;

namespace RH.Pagamento.API.Data.Repository
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly PagamentoContext _context;

        public PagamentoRepository(PagamentoContext context)
        {
            _context = context;
        }

        public async Task<Conta> ObterContaPorIdPedido(Guid idPedido)
        {
            return await _context.Contas.FirstOrDefaultAsync(c => c.PedidoId == idPedido);
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

using System;

namespace RH.Pagamento.API.Application.Queries.ViewModels
{
    public class PagamentoViewModel
    {
        public Guid ContaId { get; set; }
        public decimal ValorPago { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}

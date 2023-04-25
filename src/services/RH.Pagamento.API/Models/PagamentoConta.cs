using RH.Core.DomainObjects;
using System;

namespace RH.Pagamento.API.Models
{
    public class PagamentoConta : Entity
    {   
        public Guid ContaId { get; private set; }
        public decimal ValorPago { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataPagamento { get; private set; }

        // EF Rel.
        public Conta Conta { get; set; }

        public PagamentoConta(Guid contaId, decimal valorPago, DateTime dataPagamento)
        {
            ContaId = contaId;
            ValorPago = valorPago;
            DataPagamento = dataPagamento;
        }

        internal void AssociarConta(Guid contaId)
        {
            ContaId = contaId;
        }
    }
}

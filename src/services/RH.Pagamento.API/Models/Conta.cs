using RH.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RH.Pagamento.API.Models
{
    public class Conta : Entity, IAggregateRoot
    {
        public int Codigo { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal ValorTotal { get; private set; }
        public decimal ValorPago { get; private set; }
        public bool Cancelado { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public ContaStatus ContaStatus { get; private set; }
        private readonly List<PagamentoConta> _contaPagamentos;        

        public IReadOnlyCollection<PagamentoConta> ContaPagamentos => _contaPagamentos;

        public Conta(int codigo, Guid pedidoId, Guid clienteId, decimal valorTotal, DateTime dataVencimento)
        {
            Codigo = codigo;
            PedidoId = pedidoId;
            ClienteId = clienteId;
            ValorTotal = valorTotal;
            ValorPago = 0;
            DataVencimento = dataVencimento;
            ContaStatus = ContaStatus.Pendente;
            Cancelado = false;
        }

        public void RealizarPagamento(PagamentoConta pagamento)
        {
            pagamento.AssociarConta(pagamento.ContaId);
            _contaPagamentos.Add(pagamento);
            AtualizarValorPago();
            AtualizarStatusConta();
        }

        public void AtualizarStatusConta()
        {
            if(ValorPago == 0)
            {
                ContaStatus = ContaStatus.Pendente;
                return;
            }
            if (ValorPago < ValorPago)
            {
                ContaStatus = ContaStatus.Parcial;
                return;
            }
            ContaStatus = ContaStatus.Pago;

        }
        public void AtualizarValorPago()
        {          
            ValorPago = _contaPagamentos.Sum(v => v.ValorPago);
        }

        public void PagamentoParcial()
        {
            ContaStatus = ContaStatus.Parcial;
        }

        public void PagamentoTotal()
        {
            ContaStatus = ContaStatus.Pago;
        }

        public void CancelarConta()
        {
            Cancelado = true;
        }
    }
}

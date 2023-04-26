using RH.Pagamento.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RequestHouse.Domain.Tests.Pagamento
{

    public class PagamentoTests
    {
        [Fact(DisplayName = "Adicionar Novo Pagamento Parcial em conta Pendente ")]
        [Trait("Categoria", "Pagamento")]
        public void AdicionarPagamentoParcial_ContaPendente_DeveAtualizarValorContaAtulizarStatus()
        {
            //Arange
            var conta = new Conta(1, Guid.NewGuid(), Guid.NewGuid(), 100, DateTime.Now);
            var pagamentoConta = new PagamentoConta(conta.Id, 40, DateTime.Now);

            //Act
            conta.RealizarPagamento(pagamentoConta);

            //Assert
            Assert.Equal(40, conta.ValorPago);
            Assert.Equal(ContaStatus.Parcial, conta.ContaStatus);
        }

        [Fact(DisplayName = "Adicionar Pagamento novo pagamento Parcial em conta Parcial")]
        [Trait("Categoria", "Pagamento")]
        public void AdicionarPagamentoParcial_ContaParcial_DeveAtualizarValorContaAtulizarStatus()
        {
            //Arange
            var conta = new Conta(1, Guid.NewGuid(), Guid.NewGuid(), 100, DateTime.Now);
            var pagamentoConta = new PagamentoConta(conta.Id, 40, DateTime.Now);
            conta.RealizarPagamento(pagamentoConta);
            var novoPagamentoConta = new PagamentoConta(conta.Id, 50, DateTime.Now);
            //Act
            conta.RealizarPagamento(novoPagamentoConta);

            //Assert
            Assert.Equal(90, conta.ValorPago);
            Assert.Equal(ContaStatus.Parcial, conta.ContaStatus);
        }

        [Fact(DisplayName = "Adicionar Pagamento novo Total Parcial em conta pendente")]
        [Trait("Categoria", "Pagamento")]
        public void AdicionarPagamentoTotal_ContaPendente_DeveAtualizarValorContaAtulizarStatus()
        {
            //Arange
            var conta = new Conta(1, Guid.NewGuid(), Guid.NewGuid(), 100, DateTime.Now);
            var pagamentoConta = new PagamentoConta(conta.Id, 100, DateTime.Now);           
           
            //Act
            conta.RealizarPagamento(pagamentoConta);

            //Assert
            Assert.Equal(100, conta.ValorPago);
            Assert.Equal(ContaStatus.Pago, conta.ContaStatus);
        }
    }
}

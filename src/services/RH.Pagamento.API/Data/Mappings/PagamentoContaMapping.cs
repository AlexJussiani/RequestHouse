using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH.Pagamento.API.Models;

namespace RH.Pagamento.API.Data.Mappings
{
    public class PagamentoContaMapping : IEntityTypeConfiguration<PagamentoConta>
    {
        public void Configure(EntityTypeBuilder<PagamentoConta> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ContaId)
                .HasColumnName("id_conta");

            builder.Property(c => c.DataCadastro)
                .HasColumnName("data_cadastro");

            builder.Property(c => c.DataPagamento)
                .IsRequired()
                .HasColumnName("data_pagamento");

            builder.Property(c => c.ValorPago)
                .IsRequired()
                .HasColumnName("valor_pago");

            // 1 : N => Pedido : Pagamento
            builder.HasOne(c => c.Conta)
                .WithMany(c => c.ContaPagamentos);

            builder.ToTable("contaPagamentos");

        }
    }
}

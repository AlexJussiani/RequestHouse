using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH.Pagamento.API.Models;

namespace RH.Pagamento.API.Data.Mappings
{
    public class ContaMapping : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ClienteId)
                .IsRequired()
                .HasColumnName("cliente_id");

            builder.Property(c => c.Cancelado)
                .HasDefaultValue(false)
                .IsRequired()
                .HasColumnName("cancelado");

            builder.Property(c => c.PedidoId)
                .IsRequired()
                .HasColumnName("pedido_id");

            builder.Property(c => c.ContaStatus)
                .IsRequired()
                .HasColumnName("conta_status");

            builder.Property(c => c.DataCadastro)
                .HasColumnName("data_cadastro");

            builder.Property(c => c.DataVencimento)
                .IsRequired()
                .HasColumnName("data_vencimento");

            builder.Property(c => c.ValorPago)
                .IsRequired()
                .HasColumnName("valor_pago");

            builder.Property(c => c.ValorTotal)
                .IsRequired()
                .HasColumnName("valor_total");

            // 1 : N => Pedido : PedidoItems
            builder.HasMany(c => c.ContaPagamentos)
                        .WithOne(c => c.Conta)
                        .HasForeignKey(c => c.ContaId);

            builder.ToTable("contas");
        }
    }
}

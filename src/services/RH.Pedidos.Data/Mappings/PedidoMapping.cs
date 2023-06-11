using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH.Pedidos.Domain;

namespace RH.Pedidos.Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.ClienteId)
                .HasColumnName("clienteId");

            builder.Property(c => c.ClienteId)
                .HasColumnName("clienteId");

            builder.Property(c => c.Codigo)
                .HasColumnName("codigo");

            builder.Property(c => c.DataAutorizacao)
                .HasColumnName("data_autorizacao");

            builder.Property(c => c.DataCadastro)
                .HasColumnName("data_cadastro");

            builder.Property(c => c.DataConclusao)
                .HasColumnName("data_conclusao");

            builder.Property(c => c.DataConclusao)
                .HasColumnName("data_conclusao");

            builder.Property(c => c.ValorTotal)
                .IsRequired()
                .HasColumnType("decimal(5,2)")
                .HasColumnName("valor_total");

            builder.Property(c => c.ValorAcrescimo)
                .HasColumnType("decimal(5,2)")
                .HasColumnName("valor_acrescimo");

            builder.Property(c => c.ValorDesconto)
                .HasColumnType("decimal(5,2)")
                .HasColumnName("valor_desconto");

            builder.Property(c => c.Observacoes)
                .HasColumnName("observacoes");

            builder.OwnsOne(p => p.EnderecoEntrega, e =>
            {
                e.Property(pe => pe.Logradouro)
                    .HasColumnName("logradouro");

                e.Property(pe => pe.Numero)
                     .HasColumnName("numero");

                e.Property(pe => pe.Complemento)
                    .HasColumnName("complemento");

                e.Property(pe => pe.Bairro)
                    .HasColumnName("bairro");

                e.Property(pe => pe.Cep)
                    .HasColumnName("cep");

                e.Property(pe => pe.Cidade)
                    .HasColumnName("cidade");

                e.Property(pe => pe.Estado)
                    .HasColumnName("estado");
            });

            builder.Property(c => c.Codigo)
                .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

            // 1 : N => Pedido : PedidoItems
            builder.HasMany(c => c.PedidoItems)
                        .WithOne(c => c.Pedido)
                        .HasForeignKey(c => c.PedidoId);

            builder.ToTable("Pedidos");
        }
    }
}

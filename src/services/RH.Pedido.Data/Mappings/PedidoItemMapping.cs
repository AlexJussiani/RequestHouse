using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RH.Pedidos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH.Pedidos.Data.Mappings
{
    internal class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.Quantidade)
                .HasColumnName("quantidade");

            builder.Property(c => c.PedidoId)
                .HasColumnName("pedido_id");

            builder.Property(c => c.ProdutoId)
                .HasColumnName("produto_id");

            builder.Property(c => c.ValorUnitario)
                .HasColumnName("valor_unitario");

            builder.Property(c => c.ProdutoNome)
                .IsRequired()
                .HasColumnName("produto_nome")
                .HasColumnType("varchar(250)");

            // 1 : N => Pedido : Pagamento
            builder.HasOne(c => c.Pedido)
                .WithMany(c => c.PedidoItems);

            builder.ToTable("PedidoItems");
        }
    }
}

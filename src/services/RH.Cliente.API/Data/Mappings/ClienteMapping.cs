using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH.Clientes.API.Models;
using RH.Core.DomainObjects;

namespace RH.Clientes.API.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
               .IsRequired()
               .HasColumnName("nome")
               .HasColumnType("varchar(200)");

            builder.Property(c => c.Telefone)
                .HasColumnName("telefone")
               .HasColumnType("varchar(15)");

            builder.Property(c => c.Excluido)
                .HasColumnName("excluido");

            builder.Property(c => c.EhFornecedor)
                .HasColumnName("eh_fornecedor");

            builder.Property(c => c.EhCliente)
                .HasColumnName("eh_cliente");

            builder.OwnsOne(c => c.Cpf, tf =>
            {
                tf.Property(c => c.Numero)
                    .HasMaxLength(Cpf.CpfMaxLength)
                    .HasColumnName("cpf")
                    .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Endereco)
                    .HasColumnName("email")
                    .HasColumnType($"varchar({Email.EnderecoMaxLength})");
            });

            builder.HasOne(c => c.Endereco)
                .WithOne(c => c.Cliente);

            // 1 : N => Pedido : PedidoItems
            builder.HasMany(c => c.Contatos)
                        .WithOne(c => c.Cliente)
                        .HasForeignKey(c => c.ClienteId);

            builder.ToTable("Clientes");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH.Clientes.API.Models;

namespace RH.Clientes.API.Data.Mappings
{
    public class ContatoClienteMapping : IEntityTypeConfiguration<ContatoCliente>
    {
        public void Configure(EntityTypeBuilder<ContatoCliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
               .HasColumnName("nome")
               .IsRequired()
               .HasColumnType("varchar(200)");

            builder.Property(c => c.Telefone)
                .HasColumnName("telefone")
               .HasColumnType("varchar(15)");

            builder.Property(c => c.ClienteId)
                .IsRequired()
                .HasColumnName("cliente_id");

            builder.ToTable("Contato_Cliente");
        }

       
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH.Clientes.API.Models;

namespace RH.Clientes.API.Data.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Logradouro)
                .HasColumnName("logradouro")
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Numero)
                .HasColumnName("numero")
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Cep)
                .HasColumnName("cep")
                .HasColumnType("varchar(20)");

            builder.Property(c => c.Complemento)
                .HasColumnName("complemento")
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Bairro)
                .HasColumnName("bairro")
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Cidade)
                .HasColumnName("cidade")
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Estado)
                .HasColumnName("estado")
                .HasColumnType("varchar(50)");

            builder.ToTable("Enderecos");
        }
    }
}


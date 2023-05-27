using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RH.Produtos.API.Models;

namespace RH.Produtos.API.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
               .IsRequired()
               .HasColumnName("nome")
               .HasColumnType("varchar(100)");

            builder.Property(c => c.Descricao)
                  .IsRequired()
                  .HasColumnName("descricao")
                  .HasColumnType("varchar(300)");

            builder.Property(c => c.Ativo)
                  .IsRequired()
                  .HasColumnName("ativo");

            builder.Property(c => c.Valor)
                 .IsRequired()
                 .HasColumnType("decimal(5,2)")
                 .HasColumnName("valor");

            builder.Property(c => c.DataCadastro)
                  .IsRequired()
                  .HasColumnName("data_cadastro");

            builder.Property(c => c.Saida)
                  .IsRequired()
                  .HasColumnName("saida");

            builder.Property(c => c.Entrada)
                  .IsRequired()
                  .HasColumnName("entrada");
        }  
    }
}

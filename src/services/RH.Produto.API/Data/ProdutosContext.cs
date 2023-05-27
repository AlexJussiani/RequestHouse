using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using RH.Core.Data;
using RH.Core.Messages;
using RH.Produtos.API.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RH.Produtos.API.Data
{
    public sealed class ProdutosContext : DbContext, IUnitOfWork
    {

        public ProdutosContext(DbContextOptions<ProdutosContext> options)
            : base(options)
        {}

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProdutosContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries()
                .Where(Entry => Entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }
            return await base.SaveChangesAsync() > 0;
        }
    }
}

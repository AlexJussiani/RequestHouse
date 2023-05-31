﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RH.Clientes.API.Data;

namespace RH.Clientes.API.Migrations
{
    [DbContext(typeof(ClientesContext))]
    partial class ClientesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RH.Clientes.API.Models.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("EhCliente")
                        .HasColumnType("bit")
                        .HasColumnName("eh_cliente");

                    b.Property<bool>("EhFornecedor")
                        .HasColumnType("bit")
                        .HasColumnName("eh_fornecedor");

                    b.Property<bool>("Excluido")
                        .HasColumnType("bit")
                        .HasColumnName("excluido");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("nome");

                    b.Property<string>("Telefone")
                        .HasColumnType("varchar(15)")
                        .HasColumnName("telefone");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("RH.Clientes.API.Models.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("bairro");

                    b.Property<string>("Cep")
                        .HasColumnType("varchar(20)")
                        .HasColumnName("cep");

                    b.Property<string>("Cidade")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("cidade");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(250)")
                        .HasColumnName("complemento");

                    b.Property<string>("Estado")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("estado");

                    b.Property<string>("Logradouro")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("logradouro");

                    b.Property<string>("Numero")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("numero");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId")
                        .IsUnique();

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("RH.Clientes.API.Models.Cliente", b =>
                {
                    b.OwnsOne("RH.Core.DomainObjects.Cpf", "Cpf", b1 =>
                        {
                            b1.Property<Guid>("ClienteId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Numero")
                                .HasMaxLength(11)
                                .HasColumnType("varchar(11)")
                                .HasColumnName("cpf");

                            b1.HasKey("ClienteId");

                            b1.ToTable("Clientes");

                            b1.WithOwner()
                                .HasForeignKey("ClienteId");
                        });

                    b.OwnsOne("RH.Core.DomainObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("ClienteId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Endereco")
                                .HasColumnType("varchar(254)")
                                .HasColumnName("email");

                            b1.HasKey("ClienteId");

                            b1.ToTable("Clientes");

                            b1.WithOwner()
                                .HasForeignKey("ClienteId");
                        });

                    b.Navigation("Cpf");

                    b.Navigation("Email");
                });

            modelBuilder.Entity("RH.Clientes.API.Models.Endereco", b =>
                {
                    b.HasOne("RH.Clientes.API.Models.Cliente", "Cliente")
                        .WithOne("Endereco")
                        .HasForeignKey("RH.Clientes.API.Models.Endereco", "ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("RH.Clientes.API.Models.Cliente", b =>
                {
                    b.Navigation("Endereco");
                });
#pragma warning restore 612, 618
        }
    }
}

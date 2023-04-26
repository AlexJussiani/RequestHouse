﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RH.Pagamento.API.Data;

namespace RH.Pagamento.API.Migrations
{
    [DbContext(typeof(PagamentoContext))]
    [Migration("20230426005608_ajuste")]
    partial class ajuste
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RH.Pagamento.API.Models.Conta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Cancelado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("cancelado");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("cliente_id");

                    b.Property<int>("Codigo")
                        .HasColumnType("int");

                    b.Property<int>("ContaStatus")
                        .HasColumnType("int")
                        .HasColumnName("conta_status");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_cadastro");

                    b.Property<DateTime>("DataVencimento")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_vencimento");

                    b.Property<Guid>("PedidoId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("pedido_id");

                    b.Property<decimal>("ValorPago")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("valor_pago");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("valor_total");

                    b.HasKey("Id");

                    b.ToTable("contas");
                });

            modelBuilder.Entity("RH.Pagamento.API.Models.PagamentoConta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContaId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id_conta");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_cadastro");

                    b.Property<DateTime>("DataPagamento")
                        .HasColumnType("datetime2")
                        .HasColumnName("data_pagamento");

                    b.Property<decimal>("ValorPago")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("valor_pago");

                    b.HasKey("Id");

                    b.HasIndex("ContaId");

                    b.ToTable("contaPagamentos");
                });

            modelBuilder.Entity("RH.Pagamento.API.Models.PagamentoConta", b =>
                {
                    b.HasOne("RH.Pagamento.API.Models.Conta", "Conta")
                        .WithMany("ContaPagamentos")
                        .HasForeignKey("ContaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conta");
                });

            modelBuilder.Entity("RH.Pagamento.API.Models.Conta", b =>
                {
                    b.Navigation("ContaPagamentos");
                });
#pragma warning restore 612, 618
        }
    }
}
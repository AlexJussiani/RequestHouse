using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RH.Pagamento.API.Migrations
{
    public partial class Pagamentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    pedido_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cliente_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    valor_total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    valor_pago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_vencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    conta_status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contaPagamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_conta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    valor_pago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_pagamento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contaPagamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contaPagamentos_contas_id_conta",
                        column: x => x.id_conta,
                        principalTable: "contas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contaPagamentos_id_conta",
                table: "contaPagamentos",
                column: "id_conta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contaPagamentos");

            migrationBuilder.DropTable(
                name: "contas");
        }
    }
}

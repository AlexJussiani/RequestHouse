using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RH.Pedidos.Data.Migrations
{
    public partial class Pedidos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "MinhaSequencia",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    codigo = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR MinhaSequencia"),
                    clienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_autorizacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    data_conclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PedidoStatus = table.Column<int>(type: "int", nullable: false),
                    logradouro = table.Column<string>(type: "varchar(100)", nullable: true),
                    numero = table.Column<string>(type: "varchar(100)", nullable: true),
                    complemento = table.Column<string>(type: "varchar(100)", nullable: true),
                    bairro = table.Column<string>(type: "varchar(100)", nullable: true),
                    cep = table.Column<string>(type: "varchar(100)", nullable: true),
                    cidade = table.Column<string>(type: "varchar(100)", nullable: true),
                    estado = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PedidoItems",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pedido_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    produto_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    produto_nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    quantidade = table.Column<int>(type: "int", nullable: false),
                    valor_unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_PedidoItems_Pedidos_pedido_id",
                        column: x => x.pedido_id,
                        principalTable: "Pedidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItems_pedido_id",
                table: "PedidoItems",
                column: "pedido_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoItems");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropSequence(
                name: "MinhaSequencia");
        }
    }
}

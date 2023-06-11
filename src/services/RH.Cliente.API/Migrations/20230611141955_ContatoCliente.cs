using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RH.Clientes.API.Migrations
{
    public partial class ContatoCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contato_Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    telefone = table.Column<string>(type: "varchar(15)", nullable: true),
                    cliente_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contato_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contato_Cliente_Clientes_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contato_Cliente_cliente_id",
                table: "Contato_Cliente",
                column: "cliente_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contato_Cliente");
        }
    }
}

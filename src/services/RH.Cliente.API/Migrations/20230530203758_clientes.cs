using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RH.Clientes.API.Migrations
{
    public partial class clientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    email = table.Column<string>(type: "varchar(254)", nullable: true),
                    cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    telefone = table.Column<string>(type: "varchar(15)", nullable: true),
                    excluido = table.Column<bool>(type: "bit", nullable: false),
                    eh_fornecedor = table.Column<bool>(type: "bit", nullable: false),
                    eh_cliente = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    logradouro = table.Column<string>(type: "varchar(200)", nullable: true),
                    numero = table.Column<string>(type: "varchar(50)", nullable: true),
                    complemento = table.Column<string>(type: "varchar(250)", nullable: true),
                    bairro = table.Column<string>(type: "varchar(100)", nullable: true),
                    cep = table.Column<string>(type: "varchar(20)", nullable: true),
                    cidade = table.Column<string>(type: "varchar(100)", nullable: true),
                    estado = table.Column<string>(type: "varchar(50)", nullable: true),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enderecos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_ClienteId",
                table: "Enderecos",
                column: "ClienteId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}

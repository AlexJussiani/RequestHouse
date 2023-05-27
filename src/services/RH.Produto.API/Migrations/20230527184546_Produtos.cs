using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RH.Produtos.API.Migrations
{
    public partial class Produtos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    descricao = table.Column<string>(type: "varchar(300)", nullable: false),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    valor = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    saida = table.Column<bool>(type: "bit", nullable: false),
                    entrada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}

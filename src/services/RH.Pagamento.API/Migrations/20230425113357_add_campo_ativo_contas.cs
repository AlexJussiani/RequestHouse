using Microsoft.EntityFrameworkCore.Migrations;

namespace RH.Pagamento.API.Migrations
{
    public partial class add_campo_ativo_contas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "contas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "contas");
        }
    }
}

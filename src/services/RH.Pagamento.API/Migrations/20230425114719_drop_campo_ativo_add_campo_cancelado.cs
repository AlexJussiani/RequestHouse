using Microsoft.EntityFrameworkCore.Migrations;

namespace RH.Pagamento.API.Migrations
{
    public partial class drop_campo_ativo_add_campo_cancelado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "contas");

            migrationBuilder.AddColumn<bool>(
                name: "cancelado",
                table: "contas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cancelado",
                table: "contas");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "contas",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }
    }
}

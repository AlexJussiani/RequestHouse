using Microsoft.EntityFrameworkCore.Migrations;

namespace RH.Pedidos.Data.Migrations
{
    public partial class AdicionandoOpcaoDescontoAcrecimoPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorTotal",
                table: "Pedidos",
                newName: "valor_total");

            migrationBuilder.AlterColumn<decimal>(
                name: "valor_total",
                table: "Pedidos",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "observacoes",
                table: "Pedidos",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "valor_acrescimo",
                table: "Pedidos",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "valor_desconto",
                table: "Pedidos",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "observacoes",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "valor_acrescimo",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "valor_desconto",
                table: "Pedidos");

            migrationBuilder.RenameColumn(
                name: "valor_total",
                table: "Pedidos",
                newName: "ValorTotal");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorTotal",
                table: "Pedidos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");
        }
    }
}

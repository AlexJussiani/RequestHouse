using Microsoft.EntityFrameworkCore.Migrations;

namespace RH.Pedidos.Data.Migrations
{
    public partial class Alteracao_Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItems_Pedidos_pedido_id",
                table: "PedidoItems");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItems_Pedidos_pedido_id",
                table: "PedidoItems",
                column: "pedido_id",
                principalTable: "Pedidos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItems_Pedidos_pedido_id",
                table: "PedidoItems");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItems_Pedidos_pedido_id",
                table: "PedidoItems",
                column: "pedido_id",
                principalTable: "Pedidos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

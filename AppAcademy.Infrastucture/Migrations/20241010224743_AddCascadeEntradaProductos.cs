using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAcademy.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeEntradaProductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntradaProductos_Entradas_EntradaId",
                table: "EntradaProductos");

            migrationBuilder.AddForeignKey(
                name: "FK_EntradaProductos_Entradas_EntradaId",
                table: "EntradaProductos",
                column: "EntradaId",
                principalTable: "Entradas",
                principalColumn: "EntradaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntradaProductos_Entradas_EntradaId",
                table: "EntradaProductos");

            migrationBuilder.AddForeignKey(
                name: "FK_EntradaProductos_Entradas_EntradaId",
                table: "EntradaProductos",
                column: "EntradaId",
                principalTable: "Entradas",
                principalColumn: "EntradaId");
        }
    }
}

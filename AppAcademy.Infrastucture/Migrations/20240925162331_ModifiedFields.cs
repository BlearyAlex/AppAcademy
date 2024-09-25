using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAcademy.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCortes_Ventas_ventaId",
                table: "DetalleCortes");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePagos_Ventas_ventaId",
                table: "DetallePagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_Ventas_ventaId",
                table: "Devoluciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Ubicaciones_OrigenUbicacionId",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "StockMaximo",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "OrigenUbicacionId",
                table: "Entradas",
                newName: "OrigenId");

            migrationBuilder.RenameIndex(
                name: "IX_Entradas_OrigenUbicacionId",
                table: "Entradas",
                newName: "IX_Entradas_OrigenId");

            migrationBuilder.RenameColumn(
                name: "ventaId",
                table: "Devoluciones",
                newName: "VentaId");

            migrationBuilder.RenameIndex(
                name: "IX_Devoluciones_ventaId",
                table: "Devoluciones",
                newName: "IX_Devoluciones_VentaId");

            migrationBuilder.RenameColumn(
                name: "ventaId",
                table: "DetallePagos",
                newName: "VentaId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallePagos_ventaId",
                table: "DetallePagos",
                newName: "IX_DetallePagos_VentaId");

            migrationBuilder.RenameColumn(
                name: "ventaId",
                table: "DetalleCortes",
                newName: "VentaId");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleCortes_ventaId",
                table: "DetalleCortes",
                newName: "IX_DetalleCortes_VentaId");

            migrationBuilder.AddColumn<decimal>(
                name: "Utilidad",
                table: "Productos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "VentaId",
                table: "DetalleCortes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCortes_Ventas_VentaId",
                table: "DetalleCortes",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "ventaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePagos_Ventas_VentaId",
                table: "DetallePagos",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "ventaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_Ventas_VentaId",
                table: "Devoluciones",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "ventaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Ubicaciones_OrigenId",
                table: "Entradas",
                column: "OrigenId",
                principalTable: "Ubicaciones",
                principalColumn: "UbicacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCortes_Ventas_VentaId",
                table: "DetalleCortes");

            migrationBuilder.DropForeignKey(
                name: "FK_DetallePagos_Ventas_VentaId",
                table: "DetallePagos");

            migrationBuilder.DropForeignKey(
                name: "FK_Devoluciones_Ventas_VentaId",
                table: "Devoluciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Ubicaciones_OrigenId",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "Utilidad",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "OrigenId",
                table: "Entradas",
                newName: "OrigenUbicacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Entradas_OrigenId",
                table: "Entradas",
                newName: "IX_Entradas_OrigenUbicacionId");

            migrationBuilder.RenameColumn(
                name: "VentaId",
                table: "Devoluciones",
                newName: "ventaId");

            migrationBuilder.RenameIndex(
                name: "IX_Devoluciones_VentaId",
                table: "Devoluciones",
                newName: "IX_Devoluciones_ventaId");

            migrationBuilder.RenameColumn(
                name: "VentaId",
                table: "DetallePagos",
                newName: "ventaId");

            migrationBuilder.RenameIndex(
                name: "IX_DetallePagos_VentaId",
                table: "DetallePagos",
                newName: "IX_DetallePagos_ventaId");

            migrationBuilder.RenameColumn(
                name: "VentaId",
                table: "DetalleCortes",
                newName: "ventaId");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleCortes_VentaId",
                table: "DetalleCortes",
                newName: "IX_DetalleCortes_ventaId");

            migrationBuilder.AddColumn<int>(
                name: "StockMaximo",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ventaId",
                table: "DetalleCortes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Telefono",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCortes_Ventas_ventaId",
                table: "DetalleCortes",
                column: "ventaId",
                principalTable: "Ventas",
                principalColumn: "ventaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePagos_Ventas_ventaId",
                table: "DetallePagos",
                column: "ventaId",
                principalTable: "Ventas",
                principalColumn: "ventaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devoluciones_Ventas_ventaId",
                table: "Devoluciones",
                column: "ventaId",
                principalTable: "Ventas",
                principalColumn: "ventaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Ubicaciones_OrigenUbicacionId",
                table: "Entradas",
                column: "OrigenUbicacionId",
                principalTable: "Ubicaciones",
                principalColumn: "UbicacionId");
        }
    }
}

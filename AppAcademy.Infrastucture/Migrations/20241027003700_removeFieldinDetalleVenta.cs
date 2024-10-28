using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAcademy.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class removeFieldinDetalleVenta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoCorte",
                table: "DetalleVentas");

            migrationBuilder.DropColumn(
                name: "EstadoTipoPago",
                table: "DetalleVentas");

            migrationBuilder.AddColumn<int>(
                name: "EstadoTipoPago",
                table: "Ventas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstadoTipoPago",
                table: "Ventas");

            migrationBuilder.AddColumn<int>(
                name: "EstadoCorte",
                table: "DetalleVentas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstadoTipoPago",
                table: "DetalleVentas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAcademy.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedFieldInColegiatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Monto",
                table: "Colegiaturas",
                newName: "SaldoPendiente");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Colegiaturas",
                newName: "FechaVencimiento");

            migrationBuilder.AddColumn<int>(
                name: "Anio",
                table: "Colegiaturas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPago",
                table: "Colegiaturas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Mes",
                table: "Colegiaturas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MontoPagado",
                table: "Colegiaturas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MontoTotal",
                table: "Colegiaturas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Notas",
                table: "Colegiaturas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anio",
                table: "Colegiaturas");

            migrationBuilder.DropColumn(
                name: "FechaPago",
                table: "Colegiaturas");

            migrationBuilder.DropColumn(
                name: "Mes",
                table: "Colegiaturas");

            migrationBuilder.DropColumn(
                name: "MontoPagado",
                table: "Colegiaturas");

            migrationBuilder.DropColumn(
                name: "MontoTotal",
                table: "Colegiaturas");

            migrationBuilder.DropColumn(
                name: "Notas",
                table: "Colegiaturas");

            migrationBuilder.RenameColumn(
                name: "SaldoPendiente",
                table: "Colegiaturas",
                newName: "Monto");

            migrationBuilder.RenameColumn(
                name: "FechaVencimiento",
                table: "Colegiaturas",
                newName: "Fecha");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAcademy.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFieldEntrada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaDeEntrega",
                table: "Entradas");

            migrationBuilder.DropColumn(
                name: "VencimientoPago",
                table: "Entradas");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDeEmision",
                table: "Entradas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaDeEmision",
                table: "Entradas");

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaDeEntrega",
                table: "Entradas",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "VencimientoPago",
                table: "Entradas",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAcademy.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldInClientModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NombreCompleto",
                table: "Clientes",
                newName: "Nombre");

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Clientes",
                newName: "NombreCompleto");
        }
    }
}

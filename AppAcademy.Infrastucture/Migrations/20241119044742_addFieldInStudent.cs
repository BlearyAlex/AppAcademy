using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAcademy.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class addFieldInStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Estudiantes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Estudiantes");
        }
    }
}

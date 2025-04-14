using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAcademy.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFieldInAcademicCycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bitacora_AspNetUsers_UsuarioId",
                table: "Bitacora");

            migrationBuilder.DropIndex(
                name: "IX_Bitacora_UsuarioId",
                table: "Bitacora");

            migrationBuilder.DropColumn(
                name: "NumeroCiclo",
                table: "AcademicCycles");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Bitacora",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CicloAcademico",
                table: "AcademicCycles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CicloAcademico",
                table: "AcademicCycles");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Bitacora",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "NumeroCiclo",
                table: "AcademicCycles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bitacora_UsuarioId",
                table: "Bitacora",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bitacora_AspNetUsers_UsuarioId",
                table: "Bitacora",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

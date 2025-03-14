using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAcademy.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class FieldInStudentAddedAcademicCycle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AcademicCycleId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_AcademicCycleId",
                table: "Students",
                column: "AcademicCycleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AcademicCycles_AcademicCycleId",
                table: "Students",
                column: "AcademicCycleId",
                principalTable: "AcademicCycles",
                principalColumn: "AcademicCycleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_AcademicCycles_AcademicCycleId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_AcademicCycleId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AcademicCycleId",
                table: "Students");
        }
    }
}

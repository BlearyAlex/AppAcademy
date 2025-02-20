using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAcademy.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class FieldVentaIdChangeInAbonoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abono_Ventas_VentaId1",
                table: "Abono");

            migrationBuilder.DropIndex(
                name: "IX_Abono_VentaId1",
                table: "Abono");

            migrationBuilder.DropColumn(
                name: "VentaId1",
                table: "Abono");

            migrationBuilder.AlterColumn<string>(
                name: "VentaId",
                table: "Abono",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Abono_VentaId",
                table: "Abono",
                column: "VentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abono_Ventas_VentaId",
                table: "Abono",
                column: "VentaId",
                principalTable: "Ventas",
                principalColumn: "VentaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abono_Ventas_VentaId",
                table: "Abono");

            migrationBuilder.DropIndex(
                name: "IX_Abono_VentaId",
                table: "Abono");

            migrationBuilder.AlterColumn<int>(
                name: "VentaId",
                table: "Abono",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "VentaId1",
                table: "Abono",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Abono_VentaId1",
                table: "Abono",
                column: "VentaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Abono_Ventas_VentaId1",
                table: "Abono",
                column: "VentaId1",
                principalTable: "Ventas",
                principalColumn: "VentaId");
        }
    }
}

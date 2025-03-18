using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAcademy.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class ModelBitacoraAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bitacora",
                columns: table => new
                {
                    BitacoraId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenciaId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoReferencia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bitacora", x => x.BitacoraId);
                    table.ForeignKey(
                        name: "FK_Bitacora_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bitacora_UsuarioId",
                table: "Bitacora",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bitacora");
        }
    }
}

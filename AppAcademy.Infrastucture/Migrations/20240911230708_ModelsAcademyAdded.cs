using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAcademy.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class ModelsAcademyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    EstudianteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.EstudianteId);
                    table.ForeignKey(
                        name: "FK_Estudiantes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    MateriaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.MateriaId);
                    table.ForeignKey(
                        name: "FK_Materias_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Colegiaturas",
                columns: table => new
                {
                    ColegiaturaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstadoColegiatura = table.Column<int>(type: "int", nullable: false),
                    EstudianteId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colegiaturas", x => x.ColegiaturaId);
                    table.ForeignKey(
                        name: "FK_Colegiaturas_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteId");
                    table.ForeignKey(
                        name: "FK_Colegiaturas_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "MaterialAdeudos",
                columns: table => new
                {
                    MaterialAdeudoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstadoMaterial = table.Column<int>(type: "int", nullable: false),
                    EstudianteId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialAdeudos", x => x.MaterialAdeudoId);
                    table.ForeignKey(
                        name: "FK_MaterialAdeudos_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteId");
                    table.ForeignKey(
                        name: "FK_MaterialAdeudos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "MateriaEstudiantes",
                columns: table => new
                {
                    Materia_EstudianteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Faltas = table.Column<int>(type: "int", nullable: false),
                    EstudianteId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MateriaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaEstudiantes", x => x.Materia_EstudianteId);
                    table.ForeignKey(
                        name: "FK_MateriaEstudiantes_Estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiantes",
                        principalColumn: "EstudianteId");
                    table.ForeignKey(
                        name: "FK_MateriaEstudiantes_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "MateriaId");
                    table.ForeignKey(
                        name: "FK_MateriaEstudiantes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colegiaturas_EstudianteId",
                table: "Colegiaturas",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Colegiaturas_UserId",
                table: "Colegiaturas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_UserId",
                table: "Estudiantes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaEstudiantes_EstudianteId",
                table: "MateriaEstudiantes",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaEstudiantes_MateriaId",
                table: "MateriaEstudiantes",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaEstudiantes_UserId",
                table: "MateriaEstudiantes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAdeudos_EstudianteId",
                table: "MaterialAdeudos",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialAdeudos_UserId",
                table: "MaterialAdeudos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Materias_UserId",
                table: "Materias",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colegiaturas");

            migrationBuilder.DropTable(
                name: "MateriaEstudiantes");

            migrationBuilder.DropTable(
                name: "MaterialAdeudos");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Estudiantes");
        }
    }
}

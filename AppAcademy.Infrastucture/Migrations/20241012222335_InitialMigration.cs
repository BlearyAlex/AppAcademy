﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppAcademy.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    MarcaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.MarcaId);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    PermisoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NamePermiso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.PermisoId);
                });

            migrationBuilder.CreateTable(
                name: "Promociones",
                columns: table => new
                {
                    PromocionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descuento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promociones", x => x.PromocionId);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    ProveedorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.ProveedorId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameRol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "Ubicaciones",
                columns: table => new
                {
                    UbicacionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones", x => x.UbicacionId);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoBarras = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Utilidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescuentoBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Impuesto = table.Column<int>(type: "int", nullable: false),
                    EstadoProducto = table.Column<int>(type: "int", nullable: false),
                    StockMinimo = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MarcaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProveedorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoId);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId");
                    table.ForeignKey(
                        name: "FK_Productos_Marca_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marca",
                        principalColumn: "MarcaId");
                    table.ForeignKey(
                        name: "FK_Productos_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "ProveedorId");
                });

            migrationBuilder.CreateTable(
                name: "PermisoRol",
                columns: table => new
                {
                    PermisosPermisoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RolesRolId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermisoRol", x => new { x.PermisosPermisoId, x.RolesRolId });
                    table.ForeignKey(
                        name: "FK_PermisoRol_Permisos_PermisosPermisoId",
                        column: x => x.PermisosPermisoId,
                        principalTable: "Permisos",
                        principalColumn: "PermisoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermisoRol_Roles_RolesRolId",
                        column: x => x.RolesRolId,
                        principalTable: "Roles",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    EstadoUser = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaUltimoAcceso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RolId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "RolId");
                });

            migrationBuilder.CreateTable(
                name: "HistorialPrecios",
                columns: table => new
                {
                    HistorialPrecioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioAnterior = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialPrecios", x => x.HistorialPrecioId);
                    table.ForeignKey(
                        name: "FK_HistorialPrecios_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId");
                });

            migrationBuilder.CreateTable(
                name: "Inventarios",
                columns: table => new
                {
                    InventarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    FechaUltimaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventarios", x => x.InventarioId);
                    table.ForeignKey(
                        name: "FK_Inventarios_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId");
                });

            migrationBuilder.CreateTable(
                name: "ProductoPromocion",
                columns: table => new
                {
                    ProductosProductoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PromocionesPromocionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductoPromocion", x => new { x.ProductosProductoId, x.PromocionesPromocionId });
                    table.ForeignKey(
                        name: "FK_ProductoPromocion_Productos_ProductosProductoId",
                        column: x => x.ProductosProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductoPromocion_Promociones_PromocionesPromocionId",
                        column: x => x.PromocionesPromocionId,
                        principalTable: "Promociones",
                        principalColumn: "PromocionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cortes",
                columns: table => new
                {
                    CorteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaCorte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalEfectivo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalTarjeta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalVales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDevoluciones = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cortes", x => x.CorteId);
                    table.ForeignKey(
                        name: "FK_Cortes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Entradas",
                columns: table => new
                {
                    EntradaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalProductosEntrada = table.Column<int>(type: "int", nullable: false),
                    FechaDeEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumeroFactura = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VencimientoPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Folio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bruto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrigenId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entradas", x => x.EntradaId);
                    table.ForeignKey(
                        name: "FK_Entradas_Ubicaciones_OrigenId",
                        column: x => x.OrigenId,
                        principalTable: "Ubicaciones",
                        principalColumn: "UbicacionId");
                    table.ForeignKey(
                        name: "FK_Entradas_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

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
                name: "HistorialAccesos",
                columns: table => new
                {
                    HistorialAccesoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaAcceso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DireccionIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dispositivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoAcceso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialAccesos", x => x.HistorialAccesoId);
                    table.ForeignKey(
                        name: "FK_HistorialAccesos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "HistorialInventarios",
                columns: table => new
                {
                    HistorialInventarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CantidadAnterior = table.Column<int>(type: "int", nullable: false),
                    CantidadNueva = table.Column<int>(type: "int", nullable: false),
                    FechaCambio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialInventarios", x => x.HistorialInventarioId);
                    table.ForeignKey(
                        name: "FK_HistorialInventarios_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId");
                    table.ForeignKey(
                        name: "FK_HistorialInventarios_Users_UserId",
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
                name: "RefreshTokens",
                columns: table => new
                {
                    RefreshTokenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Salidas",
                columns: table => new
                {
                    SalidaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalProductosSalida = table.Column<int>(type: "int", nullable: false),
                    Comentarios = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salidas", x => x.SalidaId);
                    table.ForeignKey(
                        name: "FK_Salidas_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    ventaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.ventaId);
                    table.ForeignKey(
                        name: "FK_Ventas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId");
                    table.ForeignKey(
                        name: "FK_Ventas_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "EntradaProductos",
                columns: table => new
                {
                    EntradaProductoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EntradaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradaProductos", x => x.EntradaProductoId);
                    table.ForeignKey(
                        name: "FK_EntradaProductos_Entradas_EntradaId",
                        column: x => x.EntradaId,
                        principalTable: "Entradas",
                        principalColumn: "EntradaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntradaProductos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId");
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

            migrationBuilder.CreateTable(
                name: "DetalleCortes",
                columns: table => new
                {
                    DetalleCorteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TipoPago = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CorteId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VentaId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCortes", x => x.DetalleCorteId);
                    table.ForeignKey(
                        name: "FK_DetalleCortes_Cortes_CorteId",
                        column: x => x.CorteId,
                        principalTable: "Cortes",
                        principalColumn: "CorteId");
                    table.ForeignKey(
                        name: "FK_DetalleCortes_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "ventaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallePagos",
                columns: table => new
                {
                    DetallePagoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VentaId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePagos", x => x.DetallePagoId);
                    table.ForeignKey(
                        name: "FK_DetallePagos_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "ventaId");
                });

            migrationBuilder.CreateTable(
                name: "DetalleVentas",
                columns: table => new
                {
                    DetalleVentaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DescuentoProducto = table.Column<int>(type: "int", nullable: false),
                    ventaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVentas", x => x.DetalleVentaId);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId");
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Ventas_ventaId",
                        column: x => x.ventaId,
                        principalTable: "Ventas",
                        principalColumn: "ventaId");
                });

            migrationBuilder.CreateTable(
                name: "Devoluciones",
                columns: table => new
                {
                    DevolucionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaDevolucion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VentaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devoluciones", x => x.DevolucionId);
                    table.ForeignKey(
                        name: "FK_Devoluciones_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId");
                    table.ForeignKey(
                        name: "FK_Devoluciones_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Devoluciones_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "ventaId");
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
                name: "IX_Cortes_UserId",
                table: "Cortes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCortes_CorteId",
                table: "DetalleCortes",
                column: "CorteId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCortes_VentaId",
                table: "DetalleCortes",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePagos_VentaId",
                table: "DetallePagos",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_ProductoId",
                table: "DetalleVentas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_ventaId",
                table: "DetalleVentas",
                column: "ventaId");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_ProductoId",
                table: "Devoluciones",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_UserId",
                table: "Devoluciones",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_VentaId",
                table: "Devoluciones",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaProductos_EntradaId",
                table: "EntradaProductos",
                column: "EntradaId");

            migrationBuilder.CreateIndex(
                name: "IX_EntradaProductos_ProductoId",
                table: "EntradaProductos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_OrigenId",
                table: "Entradas",
                column: "OrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_Entradas_UserId",
                table: "Entradas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_UserId",
                table: "Estudiantes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAccesos_UserId",
                table: "HistorialAccesos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialInventarios_ProductoId",
                table: "HistorialInventarios",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialInventarios_UserId",
                table: "HistorialInventarios",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialPrecios_ProductoId",
                table: "HistorialPrecios",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventarios_ProductoId",
                table: "Inventarios",
                column: "ProductoId");

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

            migrationBuilder.CreateIndex(
                name: "IX_PermisoRol_RolesRolId",
                table: "PermisoRol",
                column: "RolesRolId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductoPromocion_PromocionesPromocionId",
                table: "ProductoPromocion",
                column: "PromocionesPromocionId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_MarcaId",
                table: "Productos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ProveedorId",
                table: "Productos",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_UserId",
                table: "Salidas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RolId",
                table: "Users",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ClienteId",
                table: "Ventas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_UserId",
                table: "Ventas",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colegiaturas");

            migrationBuilder.DropTable(
                name: "DetalleCortes");

            migrationBuilder.DropTable(
                name: "DetallePagos");

            migrationBuilder.DropTable(
                name: "DetalleVentas");

            migrationBuilder.DropTable(
                name: "Devoluciones");

            migrationBuilder.DropTable(
                name: "EntradaProductos");

            migrationBuilder.DropTable(
                name: "HistorialAccesos");

            migrationBuilder.DropTable(
                name: "HistorialInventarios");

            migrationBuilder.DropTable(
                name: "HistorialPrecios");

            migrationBuilder.DropTable(
                name: "Inventarios");

            migrationBuilder.DropTable(
                name: "MateriaEstudiantes");

            migrationBuilder.DropTable(
                name: "MaterialAdeudos");

            migrationBuilder.DropTable(
                name: "PermisoRol");

            migrationBuilder.DropTable(
                name: "ProductoPromocion");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Salidas");

            migrationBuilder.DropTable(
                name: "Cortes");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Entradas");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Promociones");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Ubicaciones");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

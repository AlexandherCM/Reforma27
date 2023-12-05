using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Condominios.Migrations
{
    public partial class SinFuncion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Motor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Perfil",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Periodo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Meses = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TipoEquipo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEquipo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TipoMantenimiento",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMantenimiento", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ubicacion",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicacion", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UnidadMedida",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadMedida", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerfilID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Usuario_Perfil_PerfilID",
                        column: x => x.PerfilID,
                        principalTable: "Perfil",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Variante",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarcaID = table.Column<int>(type: "int", nullable: false),
                    MotorID = table.Column<int>(type: "int", nullable: false),
                    PeriodoID = table.Column<int>(type: "int", nullable: false),
                    TipoEquipoID = table.Column<int>(type: "int", nullable: false),
                    Capacidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variante", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Variante_Marca_MarcaID",
                        column: x => x.MarcaID,
                        principalTable: "Marca",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Variante_Motor_MotorID",
                        column: x => x.MotorID,
                        principalTable: "Motor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Variante_Periodo_PeriodoID",
                        column: x => x.PeriodoID,
                        principalTable: "Periodo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Variante_TipoEquipo_TipoEquipoID",
                        column: x => x.TipoEquipoID,
                        principalTable: "TipoEquipo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VarianteID = table.Column<int>(type: "int", nullable: false),
                    UbicacionID = table.Column<int>(type: "int", nullable: false),
                    EstatusID = table.Column<int>(type: "int", nullable: false),
                    NumSerie = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Funcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostoAdquisicion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Equipo_Estatus_EstatusID",
                        column: x => x.EstatusID,
                        principalTable: "Estatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipo_Ubicacion_UbicacionID",
                        column: x => x.UbicacionID,
                        principalTable: "Ubicacion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipo_Variante_VarianteID",
                        column: x => x.VarianteID,
                        principalTable: "Variante",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MtoProgramado",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipoID = table.Column<int>(type: "int", nullable: false),
                    UltimaAplicacion = table.Column<long>(type: "bigint", nullable: false),
                    ProximaAplicacion = table.Column<long>(type: "bigint", nullable: false),
                    Aplicado = table.Column<bool>(type: "bit", nullable: false),
                    Aplicable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MtoProgramado", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MtoProgramado_Equipo_EquipoID",
                        column: x => x.EquipoID,
                        principalTable: "Equipo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mantenimiento",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MtoProgramadoID = table.Column<int>(type: "int", nullable: false),
                    TipoMantenimientoID = table.Column<int>(type: "int", nullable: false),
                    ProveedorID = table.Column<int>(type: "int", nullable: false),
                    CostoMantenimiento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CostoReparacion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAplicacion = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimiento", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Mantenimiento_MtoProgramado_MtoProgramadoID",
                        column: x => x.MtoProgramadoID,
                        principalTable: "MtoProgramado",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mantenimiento_Proveedor_ProveedorID",
                        column: x => x.ProveedorID,
                        principalTable: "Proveedor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mantenimiento_TipoMantenimiento_TipoMantenimientoID",
                        column: x => x.TipoMantenimientoID,
                        principalTable: "TipoMantenimiento",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipo_EstatusID",
                table: "Equipo",
                column: "EstatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipo_NumSerie",
                table: "Equipo",
                column: "NumSerie",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipo_UbicacionID",
                table: "Equipo",
                column: "UbicacionID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipo_VarianteID",
                table: "Equipo",
                column: "VarianteID");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimiento_MtoProgramadoID",
                table: "Mantenimiento",
                column: "MtoProgramadoID");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimiento_ProveedorID",
                table: "Mantenimiento",
                column: "ProveedorID");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimiento_TipoMantenimientoID",
                table: "Mantenimiento",
                column: "TipoMantenimientoID");

            migrationBuilder.CreateIndex(
                name: "IX_MtoProgramado_EquipoID",
                table: "MtoProgramado",
                column: "EquipoID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PerfilID",
                table: "Usuario",
                column: "PerfilID");

            migrationBuilder.CreateIndex(
                name: "IX_Variante_MarcaID",
                table: "Variante",
                column: "MarcaID");

            migrationBuilder.CreateIndex(
                name: "IX_Variante_MotorID",
                table: "Variante",
                column: "MotorID");

            migrationBuilder.CreateIndex(
                name: "IX_Variante_PeriodoID",
                table: "Variante",
                column: "PeriodoID");

            migrationBuilder.CreateIndex(
                name: "IX_Variante_TipoEquipoID",
                table: "Variante",
                column: "TipoEquipoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mantenimiento");

            migrationBuilder.DropTable(
                name: "UnidadMedida");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "MtoProgramado");

            migrationBuilder.DropTable(
                name: "Proveedor");

            migrationBuilder.DropTable(
                name: "TipoMantenimiento");

            migrationBuilder.DropTable(
                name: "Perfil");

            migrationBuilder.DropTable(
                name: "Equipo");

            migrationBuilder.DropTable(
                name: "Estatus");

            migrationBuilder.DropTable(
                name: "Ubicacion");

            migrationBuilder.DropTable(
                name: "Variante");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "Motor");

            migrationBuilder.DropTable(
                name: "Periodo");

            migrationBuilder.DropTable(
                name: "TipoEquipo");
        }
    }
}

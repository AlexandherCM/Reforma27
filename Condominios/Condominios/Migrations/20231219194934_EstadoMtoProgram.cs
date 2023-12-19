using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Condominios.Migrations
{
    public partial class EstadoMtoProgram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Caducado",
                table: "MtoProgramado",
                newName: "Estado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "MtoProgramado",
                newName: "Caducado");
        }
    }
}

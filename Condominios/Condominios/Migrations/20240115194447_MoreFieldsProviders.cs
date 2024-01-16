using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Condominios.Migrations
{
    public partial class MoreFieldsProviders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Proveedor",
                newName: "Servicio");

            migrationBuilder.AddColumn<string>(
                name: "Contacto",
                table: "Proveedor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Empresa",
                table: "Proveedor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contacto",
                table: "Proveedor");

            migrationBuilder.DropColumn(
                name: "Empresa",
                table: "Proveedor");

            migrationBuilder.RenameColumn(
                name: "Servicio",
                table: "Proveedor",
                newName: "Nombre");
        }
    }
}

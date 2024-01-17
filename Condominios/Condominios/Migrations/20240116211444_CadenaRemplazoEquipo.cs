using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Condominios.Migrations
{
    public partial class CadenaRemplazoEquipo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CadenaRemplazado",
                table: "Equipo",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CadenaRemplazado",
                table: "Equipo");
        }
    }
}

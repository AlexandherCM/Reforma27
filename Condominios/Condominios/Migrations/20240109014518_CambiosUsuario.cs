using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Condominios.Migrations
{
    public partial class CambiosUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Restablecer",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Validado",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Restablecer",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Validado",
                table: "Usuario");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Condominios.Migrations
{
    public partial class ChangeFieldsMtos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mantenimiento_MtoProgramado_MtoProgramadoID",
                table: "Mantenimiento");

            migrationBuilder.DropIndex(
                name: "IX_Mantenimiento_MtoProgramadoID",
                table: "Mantenimiento");

            migrationBuilder.DropColumn(
                name: "MtoProgramadoID",
                table: "Mantenimiento");

            migrationBuilder.AddColumn<int>(
                name: "MantenimientoID",
                table: "MtoProgramado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MtoProgramado_MantenimientoID",
                table: "MtoProgramado",
                column: "MantenimientoID");

            migrationBuilder.AddForeignKey(
                name: "FK_MtoProgramado_Mantenimiento_MantenimientoID",
                table: "MtoProgramado",
                column: "MantenimientoID",
                principalTable: "Mantenimiento",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MtoProgramado_Mantenimiento_MantenimientoID",
                table: "MtoProgramado");

            migrationBuilder.DropIndex(
                name: "IX_MtoProgramado_MantenimientoID",
                table: "MtoProgramado");

            migrationBuilder.DropColumn(
                name: "MantenimientoID",
                table: "MtoProgramado");

            migrationBuilder.AddColumn<int>(
                name: "MtoProgramadoID",
                table: "Mantenimiento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimiento_MtoProgramadoID",
                table: "Mantenimiento",
                column: "MtoProgramadoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mantenimiento_MtoProgramado_MtoProgramadoID",
                table: "Mantenimiento",
                column: "MtoProgramadoID",
                principalTable: "MtoProgramado",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

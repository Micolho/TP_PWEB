using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAirbnb.Data.Migrations
{
    public partial class ImagemEPropriedadeListado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Imagens");

            migrationBuilder.AddColumn<bool>(
                name: "Listado",
                table: "Imoveis",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Imagens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Listado",
                table: "Imoveis");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Imagens");

            migrationBuilder.AddColumn<int>(
                name: "Path",
                table: "Imagens",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

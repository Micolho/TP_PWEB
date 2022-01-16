using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAirbnb.Data.Migrations
{
    public partial class propEntregueImovel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Entregue",
                table: "Imoveis",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Entregue",
                table: "Imoveis");
        }
    }
}

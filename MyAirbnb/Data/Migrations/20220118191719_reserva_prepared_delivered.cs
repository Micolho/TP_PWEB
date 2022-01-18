using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAirbnb.Data.Migrations
{
    public partial class reserva_prepared_delivered : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Delivered",
                table: "Reservas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Prepared",
                table: "Reservas",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delivered",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "Prepared",
                table: "Reservas");
        }
    }
}

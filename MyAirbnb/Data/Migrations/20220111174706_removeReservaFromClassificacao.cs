using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAirbnb.Data.Migrations
{
    public partial class removeReservaFromClassificacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classificacaos_Reservas_ReservaId",
                table: "Classificacaos");

            migrationBuilder.DropIndex(
                name: "IX_Classificacaos_ReservaId",
                table: "Classificacaos");

            migrationBuilder.DropColumn(
                name: "ReservaId",
                table: "Classificacaos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservaId",
                table: "Classificacaos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Classificacaos_ReservaId",
                table: "Classificacaos",
                column: "ReservaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classificacaos_Reservas_ReservaId",
                table: "Classificacaos",
                column: "ReservaId",
                principalTable: "Reservas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

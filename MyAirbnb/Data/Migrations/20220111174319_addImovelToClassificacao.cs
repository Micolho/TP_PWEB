using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAirbnb.Data.Migrations
{
    public partial class addImovelToClassificacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImovelId",
                table: "Classificacaos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classificacaos_ImovelId",
                table: "Classificacaos",
                column: "ImovelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classificacaos_Imoveis_ImovelId",
                table: "Classificacaos",
                column: "ImovelId",
                principalTable: "Imoveis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classificacaos_Imoveis_ImovelId",
                table: "Classificacaos");

            migrationBuilder.DropIndex(
                name: "IX_Classificacaos_ImovelId",
                table: "Classificacaos");

            migrationBuilder.DropColumn(
                name: "ImovelId",
                table: "Classificacaos");
        }
    }
}

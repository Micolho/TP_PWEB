using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAirbnb.Migrations
{
    public partial class ModelsImovelReserva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Imoveis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    EspacoM2 = table.Column<float>(nullable: false),
                    PrecoPorNoite = table.Column<float>(nullable: false),
                    NumeroCamas = table.Column<int>(nullable: false),
                    TemCozinha = table.Column<bool>(nullable: false),
                    TemJacuzzi = table.Column<bool>(nullable: false),
                    TemPiscina = table.Column<bool>(nullable: false),
                    HoraCheckIn = table.Column<DateTime>(nullable: false),
                    HoraCheckOut = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    TipoImovel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imoveis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataDeInicio = table.Column<DateTime>(nullable: false),
                    DataDeFim = table.Column<DateTime>(nullable: false),
                    ImovelID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Imoveis_ImovelID",
                        column: x => x.ImovelID,
                        principalTable: "Imoveis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ImovelID",
                table: "Reservas",
                column: "ImovelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Imoveis");
        }
    }
}

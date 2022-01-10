using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAirbnb.Data.Migrations
{
    public partial class AddApplicationUserAndNewModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CC",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataDeNascimento",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmpresaId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Morada",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NIF",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumeroTelemovel",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    DonoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresas_AspNetUsers_DonoId",
                        column: x => x.DonoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Checklists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: false),
                    MomentoPreparacao = table.Column<bool>(nullable: false),
                    MomentoEntrega = table.Column<bool>(nullable: false),
                    DonoId = table.Column<string>(nullable: false),
                    CategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checklists_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checklists_AspNetUsers_DonoId",
                        column: x => x.DonoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Imoveis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    EspacoM2 = table.Column<float>(nullable: false),
                    PrecoPorNoite = table.Column<float>(nullable: false),
                    NumeroCamas = table.Column<int>(nullable: false),
                    TemCozinha = table.Column<bool>(nullable: false),
                    TemJacuzzi = table.Column<bool>(nullable: false),
                    TemPiscina = table.Column<bool>(nullable: false),
                    numeroWC = table.Column<int>(nullable: false),
                    NumeroPessoas = table.Column<int>(nullable: false),
                    HoraCheckIn = table.Column<DateTime>(nullable: false),
                    HoraCheckOut = table.Column<DateTime>(nullable: false),
                    Localidade = table.Column<string>(nullable: false),
                    Rua = table.Column<string>(nullable: false),
                    TipoImovelId = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    DonoId = table.Column<string>(nullable: true),
                    ResponsavelId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imoveis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imoveis_AspNetUsers_DonoId",
                        column: x => x.DonoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Imoveis_AspNetUsers_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Imoveis_Categorias_TipoImovelId",
                        column: x => x.TipoImovelId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCheckin = table.Column<DateTime>(nullable: false),
                    DataCheckout = table.Column<DateTime>(nullable: false),
                    Confirmado = table.Column<bool>(nullable: false),
                    ImovelId = table.Column<int>(nullable: false),
                    ClienteId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_AspNetUsers_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservas_Imoveis_ImovelId",
                        column: x => x.ImovelId,
                        principalTable: "Imoveis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classificacaos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estrelas = table.Column<int>(nullable: false),
                    Comentario = table.Column<string>(nullable: true),
                    ReservaId = table.Column<int>(nullable: false),
                    UtilizadorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classificacaos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classificacaos_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classificacaos_AspNetUsers_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoneChecklists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Observacoes = table.Column<string>(nullable: true),
                    ChecklistId = table.Column<int>(nullable: true),
                    ReservaId = table.Column<int>(nullable: false),
                    ResponsavelId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoneChecklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoneChecklists_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DoneChecklists_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoneChecklists_AspNetUsers_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Imagens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<int>(nullable: false),
                    DoneChecklistId = table.Column<int>(nullable: true),
                    ImovelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imagens_DoneChecklists_DoneChecklistId",
                        column: x => x.DoneChecklistId,
                        principalTable: "DoneChecklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Imagens_Imoveis_ImovelId",
                        column: x => x.ImovelId,
                        principalTable: "Imoveis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmpresaId",
                table: "AspNetUsers",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_CategoriaId",
                table: "Checklists",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Checklists_DonoId",
                table: "Checklists",
                column: "DonoId");

            migrationBuilder.CreateIndex(
                name: "IX_Classificacaos_ReservaId",
                table: "Classificacaos",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Classificacaos_UtilizadorId",
                table: "Classificacaos",
                column: "UtilizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoneChecklists_ChecklistId",
                table: "DoneChecklists",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_DoneChecklists_ReservaId",
                table: "DoneChecklists",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_DoneChecklists_ResponsavelId",
                table: "DoneChecklists",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_DonoId",
                table: "Empresas",
                column: "DonoId");

            migrationBuilder.CreateIndex(
                name: "IX_Imagens_DoneChecklistId",
                table: "Imagens",
                column: "DoneChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_Imagens_ImovelId",
                table: "Imagens",
                column: "ImovelId");

            migrationBuilder.CreateIndex(
                name: "IX_Imoveis_DonoId",
                table: "Imoveis",
                column: "DonoId");

            migrationBuilder.CreateIndex(
                name: "IX_Imoveis_ResponsavelId",
                table: "Imoveis",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Imoveis_TipoImovelId",
                table: "Imoveis",
                column: "TipoImovelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ClienteId",
                table: "Reservas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ImovelId",
                table: "Reservas",
                column: "ImovelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Empresas_EmpresaId",
                table: "AspNetUsers",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Empresas_EmpresaId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Classificacaos");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Imagens");

            migrationBuilder.DropTable(
                name: "DoneChecklists");

            migrationBuilder.DropTable(
                name: "Checklists");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Imoveis");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmpresaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CC",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DataDeNascimento",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Morada",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NIF",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NumeroTelemovel",
                table: "AspNetUsers");
        }
    }
}

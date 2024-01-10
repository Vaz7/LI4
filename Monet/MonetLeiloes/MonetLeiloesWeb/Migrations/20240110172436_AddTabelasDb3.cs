using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonetLeiloesWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelasDb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Moradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rua = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    cidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    cod_postal = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    pais = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moradas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilizador",
                columns: table => new
                {
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    nome = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    data_nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nif = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    idMorada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizador", x => x.email);
                    table.ForeignKey(
                        name: "FK_Utilizador_Moradas_idMorada",
                        column: x => x.idMorada,
                        principalTable: "Moradas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leiloes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    data_inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_fim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    valor_base = table.Column<double>(type: "float", nullable: false),
                    emailUtilizador = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leiloes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leiloes_Utilizador_emailUtilizador",
                        column: x => x.emailUtilizador,
                        principalTable: "Utilizador",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Licitacoes",
                columns: table => new
                {
                    emailUtilizador = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    idLeilao = table.Column<int>(type: "int", nullable: false),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    valor = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licitacoes", x => new { x.emailUtilizador, x.idLeilao });
                    table.ForeignKey(
                        name: "FK_Licitacoes_Leiloes_idLeilao",
                        column: x => x.idLeilao,
                        principalTable: "Leiloes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Licitacoes_Utilizador_emailUtilizador",
                        column: x => x.emailUtilizador,
                        principalTable: "Utilizador",
                        principalColumn: "email");
                });

            migrationBuilder.CreateTable(
                name: "Quadros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ano = table.Column<int>(type: "int", nullable: false),
                    altura = table.Column<double>(type: "float", nullable: false),
                    largura = table.Column<double>(type: "float", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    moldura = table.Column<bool>(type: "bit", nullable: false),
                    autor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fotografia = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    idLeilao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quadros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quadros_Leiloes_idLeilao",
                        column: x => x.idLeilao,
                        principalTable: "Leiloes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leiloes_emailUtilizador",
                table: "Leiloes",
                column: "emailUtilizador");

            migrationBuilder.CreateIndex(
                name: "IX_Licitacoes_idLeilao",
                table: "Licitacoes",
                column: "idLeilao");

            migrationBuilder.CreateIndex(
                name: "IX_Quadros_idLeilao",
                table: "Quadros",
                column: "idLeilao");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizador_idMorada",
                table: "Utilizador",
                column: "idMorada");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Licitacoes");

            migrationBuilder.DropTable(
                name: "Quadros");

            migrationBuilder.DropTable(
                name: "Leiloes");

            migrationBuilder.DropTable(
                name: "Utilizador");

            migrationBuilder.DropTable(
                name: "Moradas");
        }
    }
}

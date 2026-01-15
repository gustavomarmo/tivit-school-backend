using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edu_connect_backend.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoFluxoMatricula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "grafico_barras_dto",
                columns: table => new
                {
                    label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "grafico_pizza_dto",
                columns: table => new
                {
                    label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "kpis_coordenador_dto",
                columns: table => new
                {
                    total_alunos = table.Column<int>(type: "int", nullable: false),
                    total_professores = table.Column<int>(type: "int", nullable: false),
                    media_geral_escola = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "solicitacao_matricula",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome_completo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    codigo_otp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    validade_otp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    endereco_completo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nome_responsavel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contato_responsavel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    escolaridade_anterior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    serie_desejada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    turno_desejado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    valor_mensalidade = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_solicitacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_solicitacao_matricula", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "documento_matricula",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    solicitacao_matricula_id = table.Column<int>(type: "int", nullable: false),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    caminho_arquivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nome_original_arquivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    validado = table.Column<bool>(type: "bit", nullable: false),
                    observacao_rejeicao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documento_matricula", x => x.id);
                    table.ForeignKey(
                        name: "FK_documento_matricula_solicitacao_matricula_solicitacao_matricula_id",
                        column: x => x.solicitacao_matricula_id,
                        principalTable: "solicitacao_matricula",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_documento_matricula_solicitacao_matricula_id",
                table: "documento_matricula",
                column: "solicitacao_matricula_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "documento_matricula");

            migrationBuilder.DropTable(
                name: "grafico_barras_dto");

            migrationBuilder.DropTable(
                name: "grafico_pizza_dto");

            migrationBuilder.DropTable(
                name: "kpis_coordenador_dto");

            migrationBuilder.DropTable(
                name: "solicitacao_matricula");
        }
    }
}

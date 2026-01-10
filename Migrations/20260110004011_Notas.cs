using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edu_connect_backend.Migrations
{
    /// <inheritdoc />
    public partial class Notas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "aviso",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mensagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_postagem = table.Column<DateTime>(type: "datetime2", nullable: false),
                    turma_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aviso", x => x.id);
                    table.ForeignKey(
                        name: "FK_aviso_turma_turma_id",
                        column: x => x.turma_id,
                        principalTable: "turma",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "nota",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_lancamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    aluno_id = table.Column<int>(type: "int", nullable: false),
                    turma_disciplina_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nota", x => x.id);
                    table.ForeignKey(
                        name: "FK_nota_aluno_aluno_id",
                        column: x => x.aluno_id,
                        principalTable: "aluno",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_nota_turma_disciplina_turma_disciplina_id",
                        column: x => x.turma_disciplina_id,
                        principalTable: "turma_disciplina",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_aviso_turma_id",
                table: "aviso",
                column: "turma_id");

            migrationBuilder.CreateIndex(
                name: "IX_nota_aluno_id",
                table: "nota",
                column: "aluno_id");

            migrationBuilder.CreateIndex(
                name: "IX_nota_turma_disciplina_id",
                table: "nota",
                column: "turma_disciplina_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aviso");

            migrationBuilder.DropTable(
                name: "nota");
        }
    }
}

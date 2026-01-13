using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edu_connect_backend.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoFrequencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "frequencia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    data_aula = table.Column<DateTime>(type: "datetime2", nullable: false),
                    presente = table.Column<bool>(type: "bit", nullable: false),
                    aluno_id = table.Column<int>(type: "int", nullable: false),
                    disciplina_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_frequencia", x => x.id);
                    table.ForeignKey(
                        name: "FK_frequencia_aluno_aluno_id",
                        column: x => x.aluno_id,
                        principalTable: "aluno",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_frequencia_disciplina_disciplina_id",
                        column: x => x.disciplina_id,
                        principalTable: "disciplina",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_frequencia_aluno_id",
                table: "frequencia",
                column: "aluno_id");

            migrationBuilder.CreateIndex(
                name: "IX_frequencia_disciplina_id",
                table: "frequencia",
                column: "disciplina_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "frequencia");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edu_connect_backend.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoConteudoAcademico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "disciplina",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    turma_id = table.Column<int>(type: "int", nullable: false),
                    professor_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_disciplina", x => x.id);
                    table.ForeignKey(
                        name: "FK_disciplina_professor_professor_id",
                        column: x => x.professor_id,
                        principalTable: "professor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_disciplina_turma_turma_id",
                        column: x => x.turma_id,
                        principalTable: "turma",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "topico",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    disciplina_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topico", x => x.id);
                    table.ForeignKey(
                        name: "FK_topico_disciplina_disciplina_id",
                        column: x => x.disciplina_id,
                        principalTable: "disciplina",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "material",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    topico_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_material", x => x.id);
                    table.ForeignKey(
                        name: "FK_material_topico_topico_id",
                        column: x => x.topico_id,
                        principalTable: "topico",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_professor_id",
                table: "disciplina",
                column: "professor_id");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_turma_id",
                table: "disciplina",
                column: "turma_id");

            migrationBuilder.CreateIndex(
                name: "IX_material_topico_id",
                table: "material",
                column: "topico_id");

            migrationBuilder.CreateIndex(
                name: "IX_topico_disciplina_id",
                table: "topico",
                column: "disciplina_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "material");

            migrationBuilder.DropTable(
                name: "topico");

            migrationBuilder.DropTable(
                name: "disciplina");
        }
    }
}

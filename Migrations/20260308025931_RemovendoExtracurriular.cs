using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edu_connect_backend.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoExtracurriular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_topico_turma_extracurricular_turma_extracurricular_id",
                table: "topico");

            migrationBuilder.DropTable(
                name: "turma_extracurricular");

            migrationBuilder.DropTable(
                name: "extracurricular");

            migrationBuilder.DropIndex(
                name: "IX_topico_turma_extracurricular_id",
                table: "topico");

            migrationBuilder.DropColumn(
                name: "turma_extracurricular_id",
                table: "topico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "turma_extracurricular_id",
                table: "topico",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "extracurricular",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_extracurricular", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "turma_extracurricular",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    extracurricular_id = table.Column<int>(type: "int", nullable: false),
                    professor_id = table.Column<int>(type: "int", nullable: false),
                    turma_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turma_extracurricular", x => x.id);
                    table.ForeignKey(
                        name: "FK_turma_extracurricular_extracurricular_extracurricular_id",
                        column: x => x.extracurricular_id,
                        principalTable: "extracurricular",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_turma_extracurricular_professor_professor_id",
                        column: x => x.professor_id,
                        principalTable: "professor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_turma_extracurricular_turma_turma_id",
                        column: x => x.turma_id,
                        principalTable: "turma",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_topico_turma_extracurricular_id",
                table: "topico",
                column: "turma_extracurricular_id");

            migrationBuilder.CreateIndex(
                name: "IX_turma_extracurricular_extracurricular_id",
                table: "turma_extracurricular",
                column: "extracurricular_id");

            migrationBuilder.CreateIndex(
                name: "IX_turma_extracurricular_professor_id",
                table: "turma_extracurricular",
                column: "professor_id");

            migrationBuilder.CreateIndex(
                name: "IX_turma_extracurricular_turma_id",
                table: "turma_extracurricular",
                column: "turma_id");

            migrationBuilder.AddForeignKey(
                name: "FK_topico_turma_extracurricular_turma_extracurricular_id",
                table: "topico",
                column: "turma_extracurricular_id",
                principalTable: "turma_extracurricular",
                principalColumn: "id");
        }
    }
}

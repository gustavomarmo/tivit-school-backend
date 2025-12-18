using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edu_connect_backend.Migrations
{
    /// <inheritdoc />
    public partial class TabelasAcademicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_disciplina_professor_professor_id",
                table: "disciplina");

            migrationBuilder.DropForeignKey(
                name: "FK_disciplina_turma_turma_id",
                table: "disciplina");

            migrationBuilder.DropForeignKey(
                name: "FK_topico_disciplina_disciplina_id",
                table: "topico");

            migrationBuilder.DropIndex(
                name: "IX_disciplina_professor_id",
                table: "disciplina");

            migrationBuilder.DropIndex(
                name: "IX_disciplina_turma_id",
                table: "disciplina");

            migrationBuilder.DropColumn(
                name: "professor_id",
                table: "disciplina");

            migrationBuilder.DropColumn(
                name: "turma_id",
                table: "disciplina");

            migrationBuilder.RenameColumn(
                name: "disciplina_id",
                table: "topico",
                newName: "turma_disciplina_id");

            migrationBuilder.RenameIndex(
                name: "IX_topico_disciplina_id",
                table: "topico",
                newName: "IX_topico_turma_disciplina_id");

            migrationBuilder.AddColumn<string>(
                name: "codigo",
                table: "disciplina",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "turma_disciplina",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    turma_id = table.Column<int>(type: "int", nullable: false),
                    disciplina_id = table.Column<int>(type: "int", nullable: false),
                    professor_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turma_disciplina", x => x.id);
                    table.ForeignKey(
                        name: "FK_turma_disciplina_disciplina_disciplina_id",
                        column: x => x.disciplina_id,
                        principalTable: "disciplina",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_turma_disciplina_professor_professor_id",
                        column: x => x.professor_id,
                        principalTable: "professor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_turma_disciplina_turma_turma_id",
                        column: x => x.turma_id,
                        principalTable: "turma",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_turma_disciplina_disciplina_id",
                table: "turma_disciplina",
                column: "disciplina_id");

            migrationBuilder.CreateIndex(
                name: "IX_turma_disciplina_professor_id",
                table: "turma_disciplina",
                column: "professor_id");

            migrationBuilder.CreateIndex(
                name: "IX_turma_disciplina_turma_id",
                table: "turma_disciplina",
                column: "turma_id");

            migrationBuilder.AddForeignKey(
                name: "FK_topico_turma_disciplina_turma_disciplina_id",
                table: "topico",
                column: "turma_disciplina_id",
                principalTable: "turma_disciplina",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_topico_turma_disciplina_turma_disciplina_id",
                table: "topico");

            migrationBuilder.DropTable(
                name: "turma_disciplina");

            migrationBuilder.DropColumn(
                name: "codigo",
                table: "disciplina");

            migrationBuilder.RenameColumn(
                name: "turma_disciplina_id",
                table: "topico",
                newName: "disciplina_id");

            migrationBuilder.RenameIndex(
                name: "IX_topico_turma_disciplina_id",
                table: "topico",
                newName: "IX_topico_disciplina_id");

            migrationBuilder.AddColumn<int>(
                name: "professor_id",
                table: "disciplina",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "turma_id",
                table: "disciplina",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_professor_id",
                table: "disciplina",
                column: "professor_id");

            migrationBuilder.CreateIndex(
                name: "IX_disciplina_turma_id",
                table: "disciplina",
                column: "turma_id");

            migrationBuilder.AddForeignKey(
                name: "FK_disciplina_professor_professor_id",
                table: "disciplina",
                column: "professor_id",
                principalTable: "professor",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_disciplina_turma_turma_id",
                table: "disciplina",
                column: "turma_id",
                principalTable: "turma",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_topico_disciplina_disciplina_id",
                table: "topico",
                column: "disciplina_id",
                principalTable: "disciplina",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

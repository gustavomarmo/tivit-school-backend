using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edu_connect_backend.Migrations
{
    /// <inheritdoc />
    public partial class BugfixDashboardProfessor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nota_disciplina_disciplina_id",
                table: "nota");

            migrationBuilder.RenameColumn(
                name: "disciplina_id",
                table: "nota",
                newName: "turma_disciplina_id");

            migrationBuilder.RenameIndex(
                name: "IX_nota_disciplina_id",
                table: "nota",
                newName: "IX_nota_turma_disciplina_id");

            migrationBuilder.AddForeignKey(
                name: "FK_nota_turma_disciplina_turma_disciplina_id",
                table: "nota",
                column: "turma_disciplina_id",
                principalTable: "turma_disciplina",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nota_turma_disciplina_turma_disciplina_id",
                table: "nota");

            migrationBuilder.RenameColumn(
                name: "turma_disciplina_id",
                table: "nota",
                newName: "disciplina_id");

            migrationBuilder.RenameIndex(
                name: "IX_nota_turma_disciplina_id",
                table: "nota",
                newName: "IX_nota_disciplina_id");

            migrationBuilder.AddForeignKey(
                name: "FK_nota_disciplina_disciplina_id",
                table: "nota",
                column: "disciplina_id",
                principalTable: "disciplina",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

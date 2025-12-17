using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edu_connect_backend.Migrations
{
    /// <inheritdoc />
    public partial class RenomeandoTabelaTurma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aluno_turmas_turmaid",
                table: "aluno");

            migrationBuilder.DropPrimaryKey(
                name: "PK_turmas",
                table: "turmas");

            migrationBuilder.RenameTable(
                name: "turmas",
                newName: "turma");

            migrationBuilder.AddPrimaryKey(
                name: "PK_turma",
                table: "turma",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_turma_turmaid",
                table: "aluno",
                column: "turmaid",
                principalTable: "turma",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aluno_turma_turmaid",
                table: "aluno");

            migrationBuilder.DropPrimaryKey(
                name: "PK_turma",
                table: "turma");

            migrationBuilder.RenameTable(
                name: "turma",
                newName: "turmas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_turmas",
                table: "turmas",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_turmas_turmaid",
                table: "aluno",
                column: "turmaid",
                principalTable: "turmas",
                principalColumn: "id");
        }
    }
}

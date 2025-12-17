using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edu_connect_backend.Migrations
{
    /// <inheritdoc />
    public partial class RenomeandoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_alunos_turmas_turmaid",
                table: "alunos");

            migrationBuilder.DropForeignKey(
                name: "FK_alunos_usuario_usuarioid",
                table: "alunos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_alunos",
                table: "alunos");

            migrationBuilder.RenameTable(
                name: "alunos",
                newName: "aluno");

            migrationBuilder.RenameIndex(
                name: "IX_alunos_usuarioid",
                table: "aluno",
                newName: "IX_aluno_usuarioid");

            migrationBuilder.RenameIndex(
                name: "IX_alunos_turmaid",
                table: "aluno",
                newName: "IX_aluno_turmaid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aluno",
                table: "aluno",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_turmas_turmaid",
                table: "aluno",
                column: "turmaid",
                principalTable: "turmas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_usuario_usuarioid",
                table: "aluno",
                column: "usuarioid",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aluno_turmas_turmaid",
                table: "aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_aluno_usuario_usuarioid",
                table: "aluno");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aluno",
                table: "aluno");

            migrationBuilder.RenameTable(
                name: "aluno",
                newName: "alunos");

            migrationBuilder.RenameIndex(
                name: "IX_aluno_usuarioid",
                table: "alunos",
                newName: "IX_alunos_usuarioid");

            migrationBuilder.RenameIndex(
                name: "IX_aluno_turmaid",
                table: "alunos",
                newName: "IX_alunos_turmaid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_alunos",
                table: "alunos",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_alunos_turmas_turmaid",
                table: "alunos",
                column: "turmaid",
                principalTable: "turmas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_alunos_usuario_usuarioid",
                table: "alunos",
                column: "usuarioid",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

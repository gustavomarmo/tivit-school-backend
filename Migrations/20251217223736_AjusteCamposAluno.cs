using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edu_connect_backend.Migrations
{
    /// <inheritdoc />
    public partial class AjusteCamposAluno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aluno_turma_turmaid",
                table: "aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_aluno_usuario_usuarioid",
                table: "aluno");

            migrationBuilder.DropIndex(
                name: "IX_aluno_turmaid",
                table: "aluno");

            migrationBuilder.DropIndex(
                name: "IX_aluno_usuarioid",
                table: "aluno");

            migrationBuilder.DropColumn(
                name: "turmaid",
                table: "aluno");

            migrationBuilder.DropColumn(
                name: "usuarioid",
                table: "aluno");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_turma_id",
                table: "aluno",
                column: "turma_id");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_usuario_id",
                table: "aluno",
                column: "usuario_id");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_turma_turma_id",
                table: "aluno",
                column: "turma_id",
                principalTable: "turma",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_usuario_usuario_id",
                table: "aluno",
                column: "usuario_id",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aluno_turma_turma_id",
                table: "aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_aluno_usuario_usuario_id",
                table: "aluno");

            migrationBuilder.DropIndex(
                name: "IX_aluno_turma_id",
                table: "aluno");

            migrationBuilder.DropIndex(
                name: "IX_aluno_usuario_id",
                table: "aluno");

            migrationBuilder.AddColumn<int>(
                name: "turmaid",
                table: "aluno",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "usuarioid",
                table: "aluno",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_aluno_turmaid",
                table: "aluno",
                column: "turmaid");

            migrationBuilder.CreateIndex(
                name: "IX_aluno_usuarioid",
                table: "aluno",
                column: "usuarioid");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_turma_turmaid",
                table: "aluno",
                column: "turmaid",
                principalTable: "turma",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_aluno_usuario_usuarioid",
                table: "aluno",
                column: "usuarioid",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

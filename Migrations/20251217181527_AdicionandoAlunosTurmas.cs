using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edu_connect_backend.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoAlunosTurmas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "senhaHash",
                table: "usuario",
                newName: "senha_hash");

            migrationBuilder.RenameColumn(
                name: "fotoUrl",
                table: "usuario",
                newName: "foto_url");

            migrationBuilder.AlterColumn<string>(
                name: "perfil",
                table: "usuario",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "turmas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ano_letivo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turmas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "alunos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    matricula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    usuario_id = table.Column<int>(type: "int", nullable: false),
                    usuarioid = table.Column<int>(type: "int", nullable: false),
                    turma_id = table.Column<int>(type: "int", nullable: true),
                    turmaid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alunos", x => x.id);
                    table.ForeignKey(
                        name: "FK_alunos_turmas_turmaid",
                        column: x => x.turmaid,
                        principalTable: "turmas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_alunos_usuario_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_alunos_turmaid",
                table: "alunos",
                column: "turmaid");

            migrationBuilder.CreateIndex(
                name: "IX_alunos_usuarioid",
                table: "alunos",
                column: "usuarioid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alunos");

            migrationBuilder.DropTable(
                name: "turmas");

            migrationBuilder.RenameColumn(
                name: "senha_hash",
                table: "usuario",
                newName: "senhaHash");

            migrationBuilder.RenameColumn(
                name: "foto_url",
                table: "usuario",
                newName: "fotoUrl");

            migrationBuilder.AlterColumn<int>(
                name: "perfil",
                table: "usuario",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

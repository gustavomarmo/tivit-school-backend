using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edu_connect_backend.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoEventos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "evento",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_fim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    turma_id = table.Column<int>(type: "int", nullable: true),
                    usuario_criador_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evento", x => x.id);
                    table.ForeignKey(
                        name: "FK_evento_turma_turma_id",
                        column: x => x.turma_id,
                        principalTable: "turma",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_evento_usuario_usuario_criador_id",
                        column: x => x.usuario_criador_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_evento_turma_id",
                table: "evento",
                column: "turma_id");

            migrationBuilder.CreateIndex(
                name: "IX_evento_usuario_criador_id",
                table: "evento",
                column: "usuario_criador_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "evento");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edu_connect_backend.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoEntregas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "entrega",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    data_entrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    arquivo_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    material_id = table.Column<int>(type: "int", nullable: false),
                    aluno_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entrega", x => x.id);
                    table.ForeignKey(
                        name: "FK_entrega_aluno_aluno_id",
                        column: x => x.aluno_id,
                        principalTable: "aluno",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_entrega_material_material_id",
                        column: x => x.material_id,
                        principalTable: "material",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_entrega_aluno_id",
                table: "entrega",
                column: "aluno_id");

            migrationBuilder.CreateIndex(
                name: "IX_entrega_material_id",
                table: "entrega",
                column: "material_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "entrega");
        }
    }
}

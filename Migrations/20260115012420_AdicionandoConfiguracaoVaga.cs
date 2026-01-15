using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edu_connect_backend.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoConfiguracaoVaga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "configuracao_vaga",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    turno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    valor_mensalidade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    vagas_totais = table.Column<int>(type: "int", nullable: false),
                    ano_letivo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_configuracao_vaga", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "configuracao_vaga");
        }
    }
}

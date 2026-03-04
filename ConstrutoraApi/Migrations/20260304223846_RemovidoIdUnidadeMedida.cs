using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstrutoraApi.Migrations
{
    /// <inheritdoc />
    public partial class RemovidoIdUnidadeMedida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrcamentosObras_UnidadeMedidas_UnidadeMedidaId",
                table: "OrcamentosObras");

            migrationBuilder.DropTable(
                name: "UnidadeMedidas");

            migrationBuilder.DropIndex(
                name: "IX_OrcamentosObras_UnidadeMedidaId",
                table: "OrcamentosObras");

            migrationBuilder.DropColumn(
                name: "UnidadeMedidaId",
                table: "OrcamentosObras");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnidadeMedidaId",
                table: "OrcamentosObras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UnidadeMedidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sigla = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeMedidas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentosObras_UnidadeMedidaId",
                table: "OrcamentosObras",
                column: "UnidadeMedidaId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrcamentosObras_UnidadeMedidas_UnidadeMedidaId",
                table: "OrcamentosObras",
                column: "UnidadeMedidaId",
                principalTable: "UnidadeMedidas",
                principalColumn: "Id");
        }
    }
}

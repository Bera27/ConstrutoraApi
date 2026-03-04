using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstrutoraApi.Migrations
{
    /// <inheritdoc />
    public partial class UnidadeMedidaString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrcamentosObras_UnidadeMedidas_UnidadeId",
                table: "OrcamentosObras");

            migrationBuilder.DropColumn(
                name: "IdUnidadeMedida",
                table: "OrcamentosObras");

            migrationBuilder.RenameColumn(
                name: "UnidadeId",
                table: "OrcamentosObras",
                newName: "UnidadeMedidaId");

            migrationBuilder.RenameIndex(
                name: "IX_OrcamentosObras_UnidadeId",
                table: "OrcamentosObras",
                newName: "IX_OrcamentosObras_UnidadeMedidaId");

            migrationBuilder.AddColumn<string>(
                name: "UnidadeMedida",
                table: "OrcamentosObras",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_OrcamentosObras_UnidadeMedidas_UnidadeMedidaId",
                table: "OrcamentosObras",
                column: "UnidadeMedidaId",
                principalTable: "UnidadeMedidas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrcamentosObras_UnidadeMedidas_UnidadeMedidaId",
                table: "OrcamentosObras");

            migrationBuilder.DropColumn(
                name: "UnidadeMedida",
                table: "OrcamentosObras");

            migrationBuilder.RenameColumn(
                name: "UnidadeMedidaId",
                table: "OrcamentosObras",
                newName: "UnidadeId");

            migrationBuilder.RenameIndex(
                name: "IX_OrcamentosObras_UnidadeMedidaId",
                table: "OrcamentosObras",
                newName: "IX_OrcamentosObras_UnidadeId");

            migrationBuilder.AddColumn<int>(
                name: "IdUnidadeMedida",
                table: "OrcamentosObras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OrcamentosObras_UnidadeMedidas_UnidadeId",
                table: "OrcamentosObras",
                column: "UnidadeId",
                principalTable: "UnidadeMedidas",
                principalColumn: "Id");
        }
    }
}

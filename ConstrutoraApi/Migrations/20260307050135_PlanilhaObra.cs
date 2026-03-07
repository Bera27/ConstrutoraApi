using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstrutoraApi.Migrations
{
    /// <inheritdoc />
    public partial class PlanilhaObra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdPlanilhaObra",
                table: "OrcamentosObras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PlanilhaObra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prazo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanilhaObra", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentosObras_IdPlanilhaObra",
                table: "OrcamentosObras",
                column: "IdPlanilhaObra");

            migrationBuilder.AddForeignKey(
                name: "FK_OrcamentosObras_PlanilhaObra_IdPlanilhaObra",
                table: "OrcamentosObras",
                column: "IdPlanilhaObra",
                principalTable: "PlanilhaObra",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrcamentosObras_PlanilhaObra_IdPlanilhaObra",
                table: "OrcamentosObras");

            migrationBuilder.DropTable(
                name: "PlanilhaObra");

            migrationBuilder.DropIndex(
                name: "IX_OrcamentosObras_IdPlanilhaObra",
                table: "OrcamentosObras");

            migrationBuilder.DropColumn(
                name: "IdPlanilhaObra",
                table: "OrcamentosObras");
        }
    }
}

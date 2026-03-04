using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstrutoraApi.Migrations
{
    /// <inheritdoc />
    public partial class PrecoUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProcoUnit",
                table: "OrcamentosObras",
                newName: "PrecoUnit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrecoUnit",
                table: "OrcamentosObras",
                newName: "ProcoUnit");
        }
    }
}

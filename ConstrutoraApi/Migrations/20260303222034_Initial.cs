using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstrutoraApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "OrcamentosObras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Servico = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Qtd = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CustoMat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustoMO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustoEquip = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustoUnitTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bdi = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    ProcoUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Peso = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IdUnidadeMedida = table.Column<int>(type: "int", nullable: false),
                    UnidadeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrcamentosObras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrcamentosObras_UnidadeMedidas_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "UnidadeMedidas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentosObras_UnidadeId",
                table: "OrcamentosObras",
                column: "UnidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrcamentosObras");

            migrationBuilder.DropTable(
                name: "UnidadeMedidas");
        }
    }
}

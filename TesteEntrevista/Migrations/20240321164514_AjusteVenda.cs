using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteEntrevista.Migrations
{
    /// <inheritdoc />
    public partial class AjusteVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendaProduto");

            migrationBuilder.AddColumn<int>(
                name: "idProduto",
                table: "Venda",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "qtdVenda",
                table: "Venda",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "vlrUnitarioVenda",
                table: "Venda",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_Venda_idProduto",
                table: "Venda",
                column: "idProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_Venda_Produto_idProduto",
                table: "Venda",
                column: "idProduto",
                principalTable: "Produto",
                principalColumn: "idProduto",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venda_Produto_idProduto",
                table: "Venda");

            migrationBuilder.DropIndex(
                name: "IX_Venda_idProduto",
                table: "Venda");

            migrationBuilder.DropColumn(
                name: "idProduto",
                table: "Venda");

            migrationBuilder.DropColumn(
                name: "qtdVenda",
                table: "Venda");

            migrationBuilder.DropColumn(
                name: "vlrUnitarioVenda",
                table: "Venda");

            migrationBuilder.CreateTable(
                name: "VendaProduto",
                columns: table => new
                {
                    IdVendaProduto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProduto = table.Column<int>(type: "int", nullable: false),
                    idVenda = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    vlrUnitario = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendaProduto", x => x.IdVendaProduto);
                    table.ForeignKey(
                        name: "FK_VendaProduto_Produto_idProduto",
                        column: x => x.idProduto,
                        principalTable: "Produto",
                        principalColumn: "idProduto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendaProduto_Venda_idVenda",
                        column: x => x.idVenda,
                        principalTable: "Venda",
                        principalColumn: "idVenda",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VendaProduto_idProduto",
                table: "VendaProduto",
                column: "idProduto");

            migrationBuilder.CreateIndex(
                name: "IX_VendaProduto_idVenda",
                table: "VendaProduto",
                column: "idVenda");
        }
    }
}

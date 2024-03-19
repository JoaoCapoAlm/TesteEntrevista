using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteEntrevista.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    idCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nmCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.idCliente);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    idProduto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dscProduto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vlrUnitario = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.idProduto);
                });

            migrationBuilder.CreateTable(
                name: "Venda",
                columns: table => new
                {
                    idVenda = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCliente = table.Column<int>(type: "int", nullable: false),
                    dthVenda = table.Column<DateTime>(type: "datetime2", nullable: false),
                    vlrTotalVenda = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venda", x => x.idVenda);
                    table.ForeignKey(
                        name: "FK_Venda_Cliente_idCliente",
                        column: x => x.idCliente,
                        principalTable: "Cliente",
                        principalColumn: "idCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendaProduto",
                columns: table => new
                {
                    IdVendaProduto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idVenda = table.Column<int>(type: "int", nullable: false),
                    idProduto = table.Column<int>(type: "int", nullable: false),
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
                name: "IX_Venda_idCliente",
                table: "Venda",
                column: "idCliente");

            migrationBuilder.CreateIndex(
                name: "IX_VendaProduto_idProduto",
                table: "VendaProduto",
                column: "idProduto");

            migrationBuilder.CreateIndex(
                name: "IX_VendaProduto_idVenda",
                table: "VendaProduto",
                column: "idVenda");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendaProduto");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Venda");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}

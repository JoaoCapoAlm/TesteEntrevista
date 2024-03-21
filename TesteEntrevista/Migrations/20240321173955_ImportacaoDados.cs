using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteEntrevista.Migrations
{
    /// <inheritdoc />
    public partial class ImportacaoDados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdImportacao",
                table: "Venda",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdImportacao",
                table: "Produto",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdImportacao",
                table: "Cliente",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdImportacao",
                table: "Venda");

            migrationBuilder.DropColumn(
                name: "IdImportacao",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "IdImportacao",
                table: "Cliente");
        }
    }
}

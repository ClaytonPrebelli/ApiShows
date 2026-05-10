using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiShows.Migrations
{
    /// <inheritdoc />
    public partial class AddNotaFiscalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NecessitaNotaFiscal",
                table: "Shows",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotaEmitida",
                table: "Shows",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NecessitaNotaFiscal",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "NotaEmitida",
                table: "Shows");
        }
    }
}

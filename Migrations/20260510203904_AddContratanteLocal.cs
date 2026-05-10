using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiShows.Migrations
{
    /// <inheritdoc />
    public partial class AddContratanteLocal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contratante",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "Local",
                table: "Shows");

            migrationBuilder.AddColumn<int>(
                name: "ContratanteId",
                table: "Shows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocalId",
                table: "Shows",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Contratantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefone = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratantes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Locais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Endereco = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locais", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_ContratanteId",
                table: "Shows",
                column: "ContratanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_LocalId",
                table: "Shows",
                column: "LocalId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratantes_Nome",
                table: "Contratantes",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Locais_Nome",
                table: "Locais",
                column: "Nome");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Contratantes_ContratanteId",
                table: "Shows",
                column: "ContratanteId",
                principalTable: "Contratantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Locais_LocalId",
                table: "Shows",
                column: "LocalId",
                principalTable: "Locais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Contratantes_ContratanteId",
                table: "Shows");

            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Locais_LocalId",
                table: "Shows");

            migrationBuilder.DropTable(
                name: "Contratantes");

            migrationBuilder.DropTable(
                name: "Locais");

            migrationBuilder.DropIndex(
                name: "IX_Shows_ContratanteId",
                table: "Shows");

            migrationBuilder.DropIndex(
                name: "IX_Shows_LocalId",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "ContratanteId",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "LocalId",
                table: "Shows");

            migrationBuilder.AddColumn<string>(
                name: "Contratante",
                table: "Shows",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Local",
                table: "Shows",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

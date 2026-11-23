using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoSzerelo.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ugyfelek",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nev = table.Column<string>(type: "TEXT", nullable: true),
                    Lakcim = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ugyfelek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Munkak",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UgyfelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Rendszam = table.Column<string>(type: "TEXT", nullable: true),
                    GyartasiEv = table.Column<int>(type: "INTEGER", nullable: false),
                    Kategoria = table.Column<int>(type: "INTEGER", nullable: false),
                    HibaLeiras = table.Column<string>(type: "TEXT", nullable: true),
                    HibaSulyossag = table.Column<int>(type: "INTEGER", nullable: false),
                    JavitasiNaplo = table.Column<string>(type: "TEXT", nullable: true),
                    Allapot = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Munkak", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Munkak_Ugyfelek_UgyfelId",
                        column: x => x.UgyfelId,
                        principalTable: "Ugyfelek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Munkak_UgyfelId",
                table: "Munkak",
                column: "UgyfelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Munkak");

            migrationBuilder.DropTable(
                name: "Ugyfelek");
        }
    }
}

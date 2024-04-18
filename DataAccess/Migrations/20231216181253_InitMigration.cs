using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kabineti",
                columns: table => new
                {
                    KabinetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kabineti", x => x.KabinetId);
                });

            migrationBuilder.CreateTable(
                name: "TipOpreme",
                columns: table => new
                {
                    TipOpremeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipOpreme", x => x.TipOpremeId);
                });

            migrationBuilder.CreateTable(
                name: "Zaposleni",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Katedra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KabinetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposleni", x => x.Email);
                    table.ForeignKey(
                        name: "FK_Zaposleni_Kabineti_KabinetId",
                        column: x => x.KabinetId,
                        principalTable: "Kabineti",
                        principalColumn: "KabinetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oprema",
                columns: table => new
                {
                    SerijskiBroj = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventarskiBroj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    TipOpremeId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oprema", x => x.SerijskiBroj);
                    table.ForeignKey(
                        name: "FK_Oprema_TipOpreme_TipOpremeId",
                        column: x => x.TipOpremeId,
                        principalTable: "TipOpreme",
                        principalColumn: "TipOpremeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zaduzivanje",
                columns: table => new
                {
                    ZaduzivanjeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SerijskiBroj = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatumZaduzivanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojKomada = table.Column<int>(type: "int", nullable: false),
                    KabinetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaduzivanje", x => new { x.Email, x.SerijskiBroj, x.DatumZaduzivanja, x.ZaduzivanjeId });
                    table.ForeignKey(
                        name: "FK_Zaduzivanje_Kabineti_KabinetId",
                        column: x => x.KabinetId,
                        principalTable: "Kabineti",
                        principalColumn: "KabinetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zaduzivanje_Oprema_SerijskiBroj",
                        column: x => x.SerijskiBroj,
                        principalTable: "Oprema",
                        principalColumn: "SerijskiBroj",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Zaduzivanje_Zaposleni_Email",
                        column: x => x.Email,
                        principalTable: "Zaposleni",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Oprema_TipOpremeId",
                table: "Oprema",
                column: "TipOpremeId");

            migrationBuilder.CreateIndex(
                name: "IX_Zaduzivanje_KabinetId",
                table: "Zaduzivanje",
                column: "KabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_Zaduzivanje_SerijskiBroj",
                table: "Zaduzivanje",
                column: "SerijskiBroj");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposleni_KabinetId",
                table: "Zaposleni",
                column: "KabinetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zaduzivanje");

            migrationBuilder.DropTable(
                name: "Oprema");

            migrationBuilder.DropTable(
                name: "Zaposleni");

            migrationBuilder.DropTable(
                name: "TipOpreme");

            migrationBuilder.DropTable(
                name: "Kabineti");
        }
    }
}

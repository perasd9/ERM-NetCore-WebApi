using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Dodatpodtipopreme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NadtipId",
                table: "TipOpreme",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TipOpreme_NadtipId",
                table: "TipOpreme",
                column: "NadtipId");

            migrationBuilder.AddForeignKey(
                name: "FK_TipOpreme_TipOpreme_NadtipId",
                table: "TipOpreme",
                column: "NadtipId",
                principalTable: "TipOpreme",
                principalColumn: "TipOpremeId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipOpreme_TipOpreme_NadtipId",
                table: "TipOpreme");

            migrationBuilder.DropIndex(
                name: "IX_TipOpreme_NadtipId",
                table: "TipOpreme");

            migrationBuilder.DropColumn(
                name: "NadtipId",
                table: "TipOpreme");
        }
    }
}

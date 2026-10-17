using Microsoft.EntityFrameworkCore.Migrations;

namespace BMF.WebCore.Admin.Migrations
{
    public partial class eventCateSeason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_EventCategory_M_EventSeason_ID_EventSeason",
                table: "M_EventCategory");

            migrationBuilder.DropIndex(
                name: "IX_M_EventCategory_ID_EventSeason",
                table: "M_EventCategory");

            migrationBuilder.DropColumn(
                name: "ID_EventSeason",
                table: "M_EventCategory");

            migrationBuilder.AddColumn<long>(
                name: "ID_EventSeason",
                table: "M_EventRound",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.CreateIndex(
                name: "IX_M_EventRound_ID_EventSeason",
                table: "M_EventRound",
                column: "ID_EventSeason");

            migrationBuilder.AddForeignKey(
                name: "FK_M_EventRound_M_EventSeason_ID_EventSeason",
                table: "M_EventRound",
                column: "ID_EventSeason",
                principalTable: "M_EventSeason",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_EventRound_M_EventSeason_ID_EventSeason",
                table: "M_EventRound");

            migrationBuilder.DropIndex(
                name: "IX_M_EventRound_ID_EventSeason",
                table: "M_EventRound");

            migrationBuilder.DropColumn(
                name: "ID_EventSeason",
                table: "M_EventRound");

            migrationBuilder.AddColumn<long>(
                name: "ID_EventSeason",
                table: "M_EventCategory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_M_EventCategory_ID_EventSeason",
                table: "M_EventCategory",
                column: "ID_EventSeason");

            migrationBuilder.AddForeignKey(
                name: "FK_M_EventCategory_M_EventSeason_ID_EventSeason",
                table: "M_EventCategory",
                column: "ID_EventSeason",
                principalTable: "M_EventSeason",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BMF.WebCore.Admin.Migrations
{
    public partial class fullUpdateAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_EventRoundSessionUserResults_M_UserAccount_M_UserAccountID",
                table: "M_EventRoundSessionUserResults");

            migrationBuilder.DropIndex(
                name: "IX_M_EventRoundSessionUserResults_M_UserAccountID",
                table: "M_EventRoundSessionUserResults");

            migrationBuilder.DropColumn(
                name: "M_UserAccountID",
                table: "M_EventRoundSessionUserResults");

            migrationBuilder.AddColumn<long>(
                name: "ID_UserAccount",
                table: "M_EventRoundSessionUserResults",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_M_EventRoundSessionUserResults_ID_UserAccount",
                table: "M_EventRoundSessionUserResults",
                column: "ID_UserAccount");

            migrationBuilder.AddForeignKey(
                name: "FK_M_EventRoundSessionUserResults_M_UserAccount_ID_UserAccount",
                table: "M_EventRoundSessionUserResults",
                column: "ID_UserAccount",
                principalTable: "M_UserAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_EventRoundSessionUserResults_M_UserAccount_ID_UserAccount",
                table: "M_EventRoundSessionUserResults");

            migrationBuilder.DropIndex(
                name: "IX_M_EventRoundSessionUserResults_ID_UserAccount",
                table: "M_EventRoundSessionUserResults");

            migrationBuilder.DropColumn(
                name: "ID_UserAccount",
                table: "M_EventRoundSessionUserResults");

            migrationBuilder.AddColumn<long>(
                name: "M_UserAccountID",
                table: "M_EventRoundSessionUserResults",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_M_EventRoundSessionUserResults_M_UserAccountID",
                table: "M_EventRoundSessionUserResults",
                column: "M_UserAccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_M_EventRoundSessionUserResults_M_UserAccount_M_UserAccountID",
                table: "M_EventRoundSessionUserResults",
                column: "M_UserAccountID",
                principalTable: "M_UserAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

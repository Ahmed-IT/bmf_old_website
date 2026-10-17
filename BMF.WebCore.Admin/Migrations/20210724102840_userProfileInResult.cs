using Microsoft.EntityFrameworkCore.Migrations;

namespace BMF.WebCore.Admin.Migrations
{
    public partial class userProfileInResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_EventRoundSessionUserResults_M_UserAccount_ID_UserAccount",
                table: "M_EventRoundSessionUserResults");

            migrationBuilder.RenameColumn(
                name: "ID_UserAccount",
                table: "M_EventRoundSessionUserResults",
                newName: "ID_UserProfile");

            migrationBuilder.RenameIndex(
                name: "IX_M_EventRoundSessionUserResults_ID_UserAccount",
                table: "M_EventRoundSessionUserResults",
                newName: "IX_M_EventRoundSessionUserResults_ID_UserProfile");

            migrationBuilder.AddForeignKey(
                name: "FK_M_EventRoundSessionUserResults_M_UserProfile_ID_UserProfile",
                table: "M_EventRoundSessionUserResults",
                column: "ID_UserProfile",
                principalTable: "M_UserProfile",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_EventRoundSessionUserResults_M_UserProfile_ID_UserProfile",
                table: "M_EventRoundSessionUserResults");

            migrationBuilder.RenameColumn(
                name: "ID_UserProfile",
                table: "M_EventRoundSessionUserResults",
                newName: "ID_UserAccount");

            migrationBuilder.RenameIndex(
                name: "IX_M_EventRoundSessionUserResults_ID_UserProfile",
                table: "M_EventRoundSessionUserResults",
                newName: "IX_M_EventRoundSessionUserResults_ID_UserAccount");

            migrationBuilder.AddForeignKey(
                name: "FK_M_EventRoundSessionUserResults_M_UserAccount_ID_UserAccount",
                table: "M_EventRoundSessionUserResults",
                column: "ID_UserAccount",
                principalTable: "M_UserAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

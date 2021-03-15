using Microsoft.EntityFrameworkCore.Migrations;

namespace BMF.WebCore.Admin.Migrations
{
    public partial class userFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_UserProfile_M_UserAccount_ID_UserAccount",
                table: "M_UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_M_UserProfile_ID_UserAccount",
                table: "M_UserProfile");

            migrationBuilder.DropColumn(
                name: "ID_UserAccount",
                table: "M_UserProfile");

            migrationBuilder.AddColumn<long>(
                name: "ID_UserProfile",
                table: "M_UserAccount",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_M_UserAccount_ID_UserProfile",
                table: "M_UserAccount",
                column: "ID_UserProfile",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_M_UserAccount_M_UserProfile_ID_UserProfile",
                table: "M_UserAccount",
                column: "ID_UserProfile",
                principalTable: "M_UserProfile",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_UserAccount_M_UserProfile_ID_UserProfile",
                table: "M_UserAccount");

            migrationBuilder.DropIndex(
                name: "IX_M_UserAccount_ID_UserProfile",
                table: "M_UserAccount");

            migrationBuilder.DropColumn(
                name: "ID_UserProfile",
                table: "M_UserAccount");

            migrationBuilder.AddColumn<long>(
                name: "ID_UserAccount",
                table: "M_UserProfile",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_M_UserProfile_ID_UserAccount",
                table: "M_UserProfile",
                column: "ID_UserAccount",
                unique: true,
                filter: "[ID_UserAccount] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_M_UserProfile_M_UserAccount_ID_UserAccount",
                table: "M_UserProfile",
                column: "ID_UserAccount",
                principalTable: "M_UserAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

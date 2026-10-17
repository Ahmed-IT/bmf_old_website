using Microsoft.EntityFrameworkCore.Migrations;

namespace BMF.WebCore.Admin.Migrations
{
    public partial class licenseToProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_UserLicense_M_UserAccount_ID_UserAccount",
                table: "M_UserLicense");

            migrationBuilder.RenameColumn(
                name: "ID_UserAccount",
                table: "M_UserLicense",
                newName: "ID_UserProfile");

            migrationBuilder.RenameIndex(
                name: "IX_M_UserLicense_ID_UserAccount",
                table: "M_UserLicense",
                newName: "IX_M_UserLicense_ID_UserProfile");

            migrationBuilder.AddForeignKey(
                name: "FK_M_UserLicense_M_UserProfile_ID_UserProfile",
                table: "M_UserLicense",
                column: "ID_UserProfile",
                principalTable: "M_UserProfile",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_UserLicense_M_UserProfile_ID_UserProfile",
                table: "M_UserLicense");

            migrationBuilder.RenameColumn(
                name: "ID_UserProfile",
                table: "M_UserLicense",
                newName: "ID_UserAccount");

            migrationBuilder.RenameIndex(
                name: "IX_M_UserLicense_ID_UserProfile",
                table: "M_UserLicense",
                newName: "IX_M_UserLicense_ID_UserAccount");

            migrationBuilder.AddForeignKey(
                name: "FK_M_UserLicense_M_UserAccount_ID_UserAccount",
                table: "M_UserLicense",
                column: "ID_UserAccount",
                principalTable: "M_UserAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

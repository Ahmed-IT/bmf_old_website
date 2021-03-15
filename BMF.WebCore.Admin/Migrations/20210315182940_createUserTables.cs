using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMF.WebCore.Admin.Migrations
{
    public partial class createUserTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "M_UserAccountID",
                table: "M_EventRoundSessionUserResults",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "M_UserProfile",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Nationality = table.Column<long>(type: "bigint", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CPR = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    Mobile_Number = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Full_Address = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Blood_Group = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Place_Of_Birth = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Photo_URL = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOnUtc = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOnUtc = table.Column<DateTime>(type: "DATETIME2(7)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_UserProfile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M_UserProfile_Country_ID_Nationality",
                        column: x => x.ID_Nationality,
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "M_UserAccount",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    Email_Address = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOnUtc = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOnUtc = table.Column<DateTime>(type: "DATETIME2(7)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_UserAccount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M_UserAccount_M_UserProfile_ID",
                        column: x => x.ID,
                        principalTable: "M_UserProfile",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_M_EventRoundSessionUserResults_M_UserAccountID",
                table: "M_EventRoundSessionUserResults",
                column: "M_UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_M_UserProfile_ID_Nationality",
                table: "M_UserProfile",
                column: "ID_Nationality");

            migrationBuilder.AddForeignKey(
                name: "FK_M_EventRoundSessionUserResults_M_UserAccount_M_UserAccountID",
                table: "M_EventRoundSessionUserResults",
                column: "M_UserAccountID",
                principalTable: "M_UserAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_EventRoundSessionUserResults_M_UserAccount_M_UserAccountID",
                table: "M_EventRoundSessionUserResults");

            migrationBuilder.DropTable(
                name: "M_UserAccount");

            migrationBuilder.DropTable(
                name: "M_UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_M_EventRoundSessionUserResults_M_UserAccountID",
                table: "M_EventRoundSessionUserResults");

            migrationBuilder.DropColumn(
                name: "M_UserAccountID",
                table: "M_EventRoundSessionUserResults");
        }
    }
}

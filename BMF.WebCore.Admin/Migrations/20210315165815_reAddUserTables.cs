using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMF.WebCore.Admin.Migrations
{
    public partial class reAddUserTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "D_LicenseType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Image_Front_URL = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Image_Back_URL = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
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
                    table.PrimaryKey("PK_D_LicenseType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "M_UserAccount",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                });

            migrationBuilder.CreateTable(
                name: "M_MyFavRaces",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_User = table.Column<long>(type: "bigint", nullable: false),
                    ID_EventRoundSession = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_M_MyFavRaces", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M_MyFavRaces_M_EventRoundSession_ID_EventRoundSession",
                        column: x => x.ID_EventRoundSession,
                        principalTable: "M_EventRoundSession",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M_MyFavRaces_M_UserAccount_ID_User",
                        column: x => x.ID_User,
                        principalTable: "M_UserAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "M_MyRaces",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_User = table.Column<long>(type: "bigint", nullable: false),
                    ID_EventRoundSession = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_M_MyRaces", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M_MyRaces_M_EventRoundSession_ID_EventRoundSession",
                        column: x => x.ID_EventRoundSession,
                        principalTable: "M_EventRoundSession",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M_MyRaces_M_UserAccount_ID_User",
                        column: x => x.ID_User,
                        principalTable: "M_UserAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "M_UserLicense",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_User = table.Column<long>(type: "bigint", nullable: false),
                    ID_LicenseType = table.Column<long>(type: "bigint", nullable: false),
                    License_Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Expire_Date = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
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
                    table.PrimaryKey("PK_M_UserLicense", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M_UserLicense_D_LicenseType_ID_LicenseType",
                        column: x => x.ID_LicenseType,
                        principalTable: "D_LicenseType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M_UserLicense_M_UserAccount_ID_User",
                        column: x => x.ID_User,
                        principalTable: "M_UserAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "M_UserProfile",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Nationality = table.Column<long>(type: "bigint", nullable: true),
                    ID_UserAccount = table.Column<long>(type: "bigint", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_M_UserProfile_M_UserAccount_ID_UserAccount",
                        column: x => x.ID_UserAccount,
                        principalTable: "M_UserAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "M_UserLicenseRenewal",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_UserLicense = table.Column<long>(type: "bigint", nullable: false),
                    Issue_Type = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Issue_Date = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
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
                    table.PrimaryKey("PK_M_UserLicenseRenewal", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M_UserLicenseRenewal_M_UserLicense_ID_UserLicense",
                        column: x => x.ID_UserLicense,
                        principalTable: "M_UserLicense",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "M_UserLicenseRenewalMedicForms",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_UserLicenseRenewal = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    PDF_File = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
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
                    table.PrimaryKey("PK_M_UserLicenseRenewalMedicForms", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M_UserLicenseRenewalMedicForms_M_UserLicenseRenewal_ID_UserLicenseRenewal",
                        column: x => x.ID_UserLicenseRenewal,
                        principalTable: "M_UserLicenseRenewal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_M_EventRoundSessionUserResults_ID_User",
                table: "M_EventRoundSessionUserResults",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "IX_M_MyFavRaces_ID_EventRoundSession",
                table: "M_MyFavRaces",
                column: "ID_EventRoundSession");

            migrationBuilder.CreateIndex(
                name: "IX_M_MyFavRaces_ID_User",
                table: "M_MyFavRaces",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "IX_M_MyRaces_ID_EventRoundSession",
                table: "M_MyRaces",
                column: "ID_EventRoundSession");

            migrationBuilder.CreateIndex(
                name: "IX_M_MyRaces_ID_User",
                table: "M_MyRaces",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "IX_M_UserLicense_ID_LicenseType",
                table: "M_UserLicense",
                column: "ID_LicenseType");

            migrationBuilder.CreateIndex(
                name: "IX_M_UserLicense_ID_User",
                table: "M_UserLicense",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "IX_M_UserLicenseRenewal_ID_UserLicense",
                table: "M_UserLicenseRenewal",
                column: "ID_UserLicense");

            migrationBuilder.CreateIndex(
                name: "IX_M_UserLicenseRenewalMedicForms_ID_UserLicenseRenewal",
                table: "M_UserLicenseRenewalMedicForms",
                column: "ID_UserLicenseRenewal");

            migrationBuilder.CreateIndex(
                name: "IX_M_UserProfile_ID_Nationality",
                table: "M_UserProfile",
                column: "ID_Nationality");

            migrationBuilder.CreateIndex(
                name: "IX_M_UserProfile_ID_UserAccount",
                table: "M_UserProfile",
                column: "ID_UserAccount",
                unique: true,
                filter: "[ID_UserAccount] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_M_EventRoundSessionUserResults_M_UserAccount_ID_User",
                table: "M_EventRoundSessionUserResults",
                column: "ID_User",
                principalTable: "M_UserAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_EventRoundSessionUserResults_M_UserAccount_ID_User",
                table: "M_EventRoundSessionUserResults");

            migrationBuilder.DropTable(
                name: "M_MyFavRaces");

            migrationBuilder.DropTable(
                name: "M_MyRaces");

            migrationBuilder.DropTable(
                name: "M_UserLicenseRenewalMedicForms");

            migrationBuilder.DropTable(
                name: "M_UserProfile");

            migrationBuilder.DropTable(
                name: "M_UserLicenseRenewal");

            migrationBuilder.DropTable(
                name: "M_UserLicense");

            migrationBuilder.DropTable(
                name: "D_LicenseType");

            migrationBuilder.DropTable(
                name: "M_UserAccount");

            migrationBuilder.DropIndex(
                name: "IX_M_EventRoundSessionUserResults_ID_User",
                table: "M_EventRoundSessionUserResults");
        }
    }
}

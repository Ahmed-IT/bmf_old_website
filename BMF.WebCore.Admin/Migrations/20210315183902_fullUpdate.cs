using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMF.WebCore.Admin.Migrations
{
    public partial class fullUpdate : Migration
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
                name: "M_MyFavRaces",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_UserAccount = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_M_MyFavRaces_M_UserAccount_ID_UserAccount",
                        column: x => x.ID_UserAccount,
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
                    ID_UserAccount = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_M_MyRaces_M_UserAccount_ID_UserAccount",
                        column: x => x.ID_UserAccount,
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
                    ID_UserAccount = table.Column<long>(type: "bigint", nullable: false),
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
                        name: "FK_M_UserLicense_M_UserAccount_ID_UserAccount",
                        column: x => x.ID_UserAccount,
                        principalTable: "M_UserAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_M_MyFavRaces_ID_EventRoundSession",
                table: "M_MyFavRaces",
                column: "ID_EventRoundSession");

            migrationBuilder.CreateIndex(
                name: "IX_M_MyFavRaces_ID_UserAccount",
                table: "M_MyFavRaces",
                column: "ID_UserAccount");

            migrationBuilder.CreateIndex(
                name: "IX_M_MyRaces_ID_EventRoundSession",
                table: "M_MyRaces",
                column: "ID_EventRoundSession");

            migrationBuilder.CreateIndex(
                name: "IX_M_MyRaces_ID_UserAccount",
                table: "M_MyRaces",
                column: "ID_UserAccount");

            migrationBuilder.CreateIndex(
                name: "IX_M_UserLicense_ID_LicenseType",
                table: "M_UserLicense",
                column: "ID_LicenseType");

            migrationBuilder.CreateIndex(
                name: "IX_M_UserLicense_ID_UserAccount",
                table: "M_UserLicense",
                column: "ID_UserAccount");

            migrationBuilder.CreateIndex(
                name: "IX_M_UserLicenseRenewal_ID_UserLicense",
                table: "M_UserLicenseRenewal",
                column: "ID_UserLicense");

            migrationBuilder.CreateIndex(
                name: "IX_M_UserLicenseRenewalMedicForms_ID_UserLicenseRenewal",
                table: "M_UserLicenseRenewalMedicForms",
                column: "ID_UserLicenseRenewal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "M_MyFavRaces");

            migrationBuilder.DropTable(
                name: "M_MyRaces");

            migrationBuilder.DropTable(
                name: "M_UserLicenseRenewalMedicForms");

            migrationBuilder.DropTable(
                name: "M_UserLicenseRenewal");

            migrationBuilder.DropTable(
                name: "M_UserLicense");

            migrationBuilder.DropTable(
                name: "D_LicenseType");
        }
    }
}

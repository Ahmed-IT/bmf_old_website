using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMF.WebCore.Admin.Migrations
{
    public partial class UserLicRenewalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "M_UserLicenseRenewalMedicForms");

            migrationBuilder.DropColumn(
                name: "Issue_Type",
                table: "M_UserLicenseRenewal");

            migrationBuilder.RenameColumn(
                name: "Issue_Date",
                table: "M_UserLicenseRenewal",
                newName: "DateOfExpiry");

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContact_MobileNumber",
                table: "M_UserProfile",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContact_Name",
                table: "M_UserProfile",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Parent_CPR",
                table: "M_UserProfile",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Parent_FullName",
                table: "M_UserProfile",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "M_UserLicenseRenewal",
                type: "int",
                maxLength: 120,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CPR_URL",
                table: "M_UserLicenseRenewal",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateIssued",
                table: "M_UserLicenseRenewal",
                type: "DATETIME2(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MedicalForm_URL",
                table: "M_UserLicenseRenewal",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoObjectionLetter_URL",
                table: "M_UserLicenseRenewal",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Parent_CPR_URL",
                table: "M_UserLicenseRenewal",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "M_EventCategoryHeat",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_EventCategory = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
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
                    table.PrimaryKey("PK_M_EventCategoryHeat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M_EventCategoryHeat_M_EventCategory_ID_EventCategory",
                        column: x => x.ID_EventCategory,
                        principalTable: "M_EventCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_M_EventCategoryHeat_ID_EventCategory",
                table: "M_EventCategoryHeat",
                column: "ID_EventCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "M_EventCategoryHeat");

            migrationBuilder.DropColumn(
                name: "EmergencyContact_MobileNumber",
                table: "M_UserProfile");

            migrationBuilder.DropColumn(
                name: "EmergencyContact_Name",
                table: "M_UserProfile");

            migrationBuilder.DropColumn(
                name: "Parent_CPR",
                table: "M_UserProfile");

            migrationBuilder.DropColumn(
                name: "Parent_FullName",
                table: "M_UserProfile");

            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "M_UserLicenseRenewal");

            migrationBuilder.DropColumn(
                name: "CPR_URL",
                table: "M_UserLicenseRenewal");

            migrationBuilder.DropColumn(
                name: "DateIssued",
                table: "M_UserLicenseRenewal");

            migrationBuilder.DropColumn(
                name: "MedicalForm_URL",
                table: "M_UserLicenseRenewal");

            migrationBuilder.DropColumn(
                name: "NoObjectionLetter_URL",
                table: "M_UserLicenseRenewal");

            migrationBuilder.DropColumn(
                name: "Parent_CPR_URL",
                table: "M_UserLicenseRenewal");

            migrationBuilder.RenameColumn(
                name: "DateOfExpiry",
                table: "M_UserLicenseRenewal",
                newName: "Issue_Date");

            migrationBuilder.AddColumn<string>(
                name: "Issue_Type",
                table: "M_UserLicenseRenewal",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "M_UserLicenseRenewalMedicForms",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    Date = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOnUtc = table.Column<DateTime>(type: "DATETIME2(7)", nullable: true),
                    ID_UserLicenseRenewal = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PDF_File = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOnUtc = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false)
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
                name: "IX_M_UserLicenseRenewalMedicForms_ID_UserLicenseRenewal",
                table: "M_UserLicenseRenewalMedicForms",
                column: "ID_UserLicenseRenewal");
        }
    }
}

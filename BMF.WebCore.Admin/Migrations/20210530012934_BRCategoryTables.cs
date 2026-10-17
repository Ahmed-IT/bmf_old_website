using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMF.WebCore.Admin.Migrations
{
    public partial class BRCategoryTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "M_EventCategoryBulletins");

            migrationBuilder.DropTable(
                name: "M_EventCategoryRules");

            migrationBuilder.CreateTable(
                name: "M_BRCategory",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
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
                    table.PrimaryKey("PK_M_BRCategory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "M_BRSubCategory",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Category = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
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
                    table.PrimaryKey("PK_M_BRSubCategory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M_BRSubCategory_M_BRCategory_ID_Category",
                        column: x => x.ID_Category,
                        principalTable: "M_BRCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "M_BRSubCategoryBulletin",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_SubCategory = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PDF_Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
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
                    table.PrimaryKey("PK_M_BRSubCategoryBulletin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M_BRSubCategoryBulletin_M_BRSubCategory_ID_SubCategory",
                        column: x => x.ID_SubCategory,
                        principalTable: "M_BRSubCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "M_BRSubCategoryRulesRegs",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_SubCategory = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PDF_Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
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
                    table.PrimaryKey("PK_M_BRSubCategoryRulesRegs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M_BRSubCategoryRulesRegs_M_BRSubCategory_ID_SubCategory",
                        column: x => x.ID_SubCategory,
                        principalTable: "M_BRSubCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_M_BRSubCategory_ID_Category",
                table: "M_BRSubCategory",
                column: "ID_Category");

            migrationBuilder.CreateIndex(
                name: "IX_M_BRSubCategoryBulletin_ID_SubCategory",
                table: "M_BRSubCategoryBulletin",
                column: "ID_SubCategory");

            migrationBuilder.CreateIndex(
                name: "IX_M_BRSubCategoryRulesRegs_ID_SubCategory",
                table: "M_BRSubCategoryRulesRegs",
                column: "ID_SubCategory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "M_BRSubCategoryBulletin");

            migrationBuilder.DropTable(
                name: "M_BRSubCategoryRulesRegs");

            migrationBuilder.DropTable(
                name: "M_BRSubCategory");

            migrationBuilder.DropTable(
                name: "M_BRCategory");

            migrationBuilder.CreateTable(
                name: "M_EventCategoryBulletins",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOnUtc = table.Column<DateTime>(type: "DATETIME2(7)", nullable: true),
                    ID_EventCategory = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PDF_Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOnUtc = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_EventCategoryBulletins", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M_EventCategoryBulletins_M_EventCategory_ID_EventCategory",
                        column: x => x.ID_EventCategory,
                        principalTable: "M_EventCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "M_EventCategoryRules",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOnUtc = table.Column<DateTime>(type: "DATETIME2(7)", nullable: true),
                    ID_EventCategory = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PDF_Url = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOnUtc = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_M_EventCategoryRules", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M_EventCategoryRules_M_EventCategory_ID_EventCategory",
                        column: x => x.ID_EventCategory,
                        principalTable: "M_EventCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_M_EventCategoryBulletins_ID_EventCategory",
                table: "M_EventCategoryBulletins",
                column: "ID_EventCategory");

            migrationBuilder.CreateIndex(
                name: "IX_M_EventCategoryRules_ID_EventCategory",
                table: "M_EventCategoryRules",
                column: "ID_EventCategory");
        }
    }
}

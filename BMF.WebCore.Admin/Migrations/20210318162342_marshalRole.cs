using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMF.WebCore.Admin.Migrations
{
    public partial class marshalRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ID_MarshalRole",
                table: "M_UserLicense",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "D_MarshalRole",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_D_MarshalRole", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_M_UserLicense_ID_MarshalRole",
                table: "M_UserLicense",
                column: "ID_MarshalRole");

            migrationBuilder.AddForeignKey(
                name: "FK_M_UserLicense_D_MarshalRole_ID_MarshalRole",
                table: "M_UserLicense",
                column: "ID_MarshalRole",
                principalTable: "D_MarshalRole",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_M_UserLicense_D_MarshalRole_ID_MarshalRole",
                table: "M_UserLicense");

            migrationBuilder.DropTable(
                name: "D_MarshalRole");

            migrationBuilder.DropIndex(
                name: "IX_M_UserLicense_ID_MarshalRole",
                table: "M_UserLicense");

            migrationBuilder.DropColumn(
                name: "ID_MarshalRole",
                table: "M_UserLicense");
        }
    }
}

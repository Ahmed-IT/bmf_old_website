using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BMF.WebCore.Admin.Migrations
{
    public partial class signInTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "M_UserRoundSignIn",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_UserProfile = table.Column<long>(type: "bigint", nullable: false),
                    ID_UserLicense = table.Column<long>(type: "bigint", nullable: false),
                    ID_EventRound = table.Column<long>(type: "bigint", nullable: false),
                    SignIn = table.Column<DateTime>(type: "DATETIME2(7)", nullable: false),
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
                    table.PrimaryKey("PK_M_UserRoundSignIn", x => x.ID);
                    table.ForeignKey(
                        name: "FK_M_UserRoundSignIn_M_EventRound_ID_EventRound",
                        column: x => x.ID_EventRound,
                        principalTable: "M_EventRound",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M_UserRoundSignIn_M_UserLicense_ID_UserLicense",
                        column: x => x.ID_UserLicense,
                        principalTable: "M_UserLicense",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M_UserRoundSignIn_M_UserProfile_ID_UserProfile",
                        column: x => x.ID_UserProfile,
                        principalTable: "M_UserProfile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_M_UserRoundSignIn_ID_EventRound",
                table: "M_UserRoundSignIn",
                column: "ID_EventRound");

            migrationBuilder.CreateIndex(
                name: "IX_M_UserRoundSignIn_ID_UserLicense",
                table: "M_UserRoundSignIn",
                column: "ID_UserLicense");

            migrationBuilder.CreateIndex(
                name: "IX_M_UserRoundSignIn_ID_UserProfile",
                table: "M_UserRoundSignIn",
                column: "ID_UserProfile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "M_UserRoundSignIn");
        }
    }
}

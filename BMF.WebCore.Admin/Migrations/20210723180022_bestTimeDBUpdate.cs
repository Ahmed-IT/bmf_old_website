using Microsoft.EntityFrameworkCore.Migrations;

namespace BMF.WebCore.Admin.Migrations
{
    public partial class bestTimeDBUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BestTime",
                table: "M_EventRoundSessionUserResults",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestTime",
                table: "M_EventRoundSessionUserResults");
        }
    }
}

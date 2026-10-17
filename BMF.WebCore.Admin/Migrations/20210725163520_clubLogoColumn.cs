using Microsoft.EntityFrameworkCore.Migrations;

namespace BMF.WebCore.Admin.Migrations
{
    public partial class clubLogoColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image_URL",
                table: "M_Club",
                newName: "Image_URL_Logo");

            migrationBuilder.AddColumn<string>(
                name: "Image_URL_Banner",
                table: "M_Club",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image_URL_Banner",
                table: "M_Club");

            migrationBuilder.RenameColumn(
                name: "Image_URL_Logo",
                table: "M_Club",
                newName: "Image_URL");
        }
    }
}

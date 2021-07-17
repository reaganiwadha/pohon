using Microsoft.EntityFrameworkCore.Migrations;

namespace Pohon.Migrations
{
    public partial class AddGithubIdToGithubOAuthDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "github_id",
                table: "github_oauth_details",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "github_id",
                table: "github_oauth_details");
        }
    }
}

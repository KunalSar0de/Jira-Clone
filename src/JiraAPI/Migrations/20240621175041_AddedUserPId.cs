using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JiraAPI.Migrations
{
    public partial class AddedUserPId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProjectAdmin",
                table: "User",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProjectAdmin",
                table: "User");
        }
    }
}

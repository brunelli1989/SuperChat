using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperChat.Web.Data.Migrations
{
    public partial class addusernamemessagedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Message",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Message");
        }
    }
}

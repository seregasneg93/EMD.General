using Microsoft.EntityFrameworkCore.Migrations;

namespace EDM.DataModel.Migrations
{
    public partial class FixDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Assignments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EDM.DataModel.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Implement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Complexity = table.Column<double>(type: "float", nullable: false),
                    Achievement = table.Column<double>(type: "float", nullable: false),
                    DateEnd = table.Column<double>(type: "float", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    ChildrenId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Assignments_ChildrenId",
                        column: x => x.ChildrenId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ChildrenId",
                table: "Assignments",
                column: "ChildrenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignments");
        }
    }
}

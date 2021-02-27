using Microsoft.EntityFrameworkCore.Migrations;

namespace EDM.DataModel.Migrations
{
    public partial class FixDB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Assignments_ChildrenId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "ChildrenId",
                table: "Assignments",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_ChildrenId",
                table: "Assignments",
                newName: "IX_Assignments_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Assignments_ParentId",
                table: "Assignments",
                column: "ParentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Assignments_ParentId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Assignments",
                newName: "ChildrenId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_ParentId",
                table: "Assignments",
                newName: "IX_Assignments_ChildrenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Assignments_ChildrenId",
                table: "Assignments",
                column: "ChildrenId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class initwithportfolio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "ExtracurricularActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExtracurricularActivities_StudentId",
                table: "ExtracurricularActivities",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtracurricularActivities_Students_StudentId",
                table: "ExtracurricularActivities",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtracurricularActivities_Students_StudentId",
                table: "ExtracurricularActivities");

            migrationBuilder.DropIndex(
                name: "IX_ExtracurricularActivities_StudentId",
                table: "ExtracurricularActivities");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "ExtracurricularActivities");
        }
    }
}

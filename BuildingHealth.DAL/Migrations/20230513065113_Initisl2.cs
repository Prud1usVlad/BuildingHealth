using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuildingHealth.DAL.Migrations
{
    public partial class Initisl2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorsResponse_BuildingProject",
                table: "SensorsResponse");

            migrationBuilder.AddForeignKey(
                name: "FK_SensorsResponse_BuildingProject",
                table: "SensorsResponse",
                column: "BuildingProjectId",
                principalTable: "BuildingProject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SensorsResponse_BuildingProject",
                table: "SensorsResponse");

            migrationBuilder.AddForeignKey(
                name: "FK_SensorsResponse_BuildingProject",
                table: "SensorsResponse",
                column: "BuildingProjectId",
                principalTable: "BuildingProject",
                principalColumn: "Id");
        }
    }
}

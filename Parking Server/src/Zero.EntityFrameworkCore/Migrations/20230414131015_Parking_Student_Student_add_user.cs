using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero.Migrations
{
    public partial class Parking_Student_Student_add_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Parking_Student_Student",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parking_Student_Student_UserId",
                table: "Parking_Student_Student",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parking_Student_Student_AbpUsers_UserId",
                table: "Parking_Student_Student",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parking_Student_Student_AbpUsers_UserId",
                table: "Parking_Student_Student");

            migrationBuilder.DropIndex(
                name: "IX_Parking_Student_Student_UserId",
                table: "Parking_Student_Student");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Parking_Student_Student");
        }
    }
}

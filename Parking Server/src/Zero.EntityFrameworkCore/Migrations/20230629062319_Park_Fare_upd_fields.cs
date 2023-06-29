using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero.Migrations
{
    public partial class Park_Fare_upd_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeekEnd",
                table: "Park_Fare");

            migrationBuilder.RenameColumn(
                name: "DayOfWeekStart",
                table: "Park_Fare",
                newName: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Park_Fare",
                newName: "DayOfWeekStart");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeekEnd",
                table: "Park_Fare",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

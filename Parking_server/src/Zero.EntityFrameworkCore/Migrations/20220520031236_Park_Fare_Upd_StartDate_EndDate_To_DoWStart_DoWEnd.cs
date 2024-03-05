using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero.Migrations
{
    public partial class Park_Fare_Upd_StartDate_EndDate_To_DoWStart_DoWEnd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Park_Fare");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Park_Fare");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeekEnd",
                table: "Park_Fare",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeekStart",
                table: "Park_Fare",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeekEnd",
                table: "Park_Fare");

            migrationBuilder.DropColumn(
                name: "DayOfWeekStart",
                table: "Park_Fare");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Park_Fare",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Park_Fare",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

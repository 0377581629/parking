using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero.Migrations
{
    public partial class Park_History_upd_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Park_History_Park_Card_CardType_CardTypeId",
                table: "Park_History");

            migrationBuilder.DropForeignKey(
                name: "FK_Park_History_Park_Vehicle_VehicleType_VehicleTypeId",
                table: "Park_History");

            migrationBuilder.DropIndex(
                name: "IX_Park_History_CardTypeId",
                table: "Park_History");

            migrationBuilder.DropColumn(
                name: "CardTypeId",
                table: "Park_History");

            migrationBuilder.DropColumn(
                name: "InTime",
                table: "Park_History");

            migrationBuilder.RenameColumn(
                name: "VehicleTypeId",
                table: "Park_History",
                newName: "CardId");

            migrationBuilder.RenameColumn(
                name: "OutTime",
                table: "Park_History",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "CardCode",
                table: "Park_History",
                newName: "Photo");

            migrationBuilder.RenameIndex(
                name: "IX_Park_History_VehicleTypeId",
                table: "Park_History",
                newName: "IX_Park_History_CardId");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Park_History",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Park_History_Park_Card_Card_CardId",
                table: "Park_History",
                column: "CardId",
                principalTable: "Park_Card_Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Park_History_Park_Card_Card_CardId",
                table: "Park_History");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Park_History");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Park_History",
                newName: "OutTime");

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Park_History",
                newName: "CardCode");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "Park_History",
                newName: "VehicleTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Park_History_CardId",
                table: "Park_History",
                newName: "IX_Park_History_VehicleTypeId");

            migrationBuilder.AddColumn<int>(
                name: "CardTypeId",
                table: "Park_History",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InTime",
                table: "Park_History",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Park_History_CardTypeId",
                table: "Park_History",
                column: "CardTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Park_History_Park_Card_CardType_CardTypeId",
                table: "Park_History",
                column: "CardTypeId",
                principalTable: "Park_Card_CardType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Park_History_Park_Vehicle_VehicleType_VehicleTypeId",
                table: "Park_History",
                column: "VehicleTypeId",
                principalTable: "Park_Vehicle_VehicleType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

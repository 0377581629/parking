using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero.Migrations
{
    public partial class Park_History_add_CardType_and_VehicleType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CardTypeId",
                table: "Park_History",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleTypeId",
                table: "Park_History",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Park_History_CardTypeId",
                table: "Park_History",
                column: "CardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Park_History_VehicleTypeId",
                table: "Park_History",
                column: "VehicleTypeId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Park_History_VehicleTypeId",
                table: "Park_History");

            migrationBuilder.DropColumn(
                name: "CardTypeId",
                table: "Park_History");

            migrationBuilder.DropColumn(
                name: "VehicleTypeId",
                table: "Park_History");
        }
    }
}

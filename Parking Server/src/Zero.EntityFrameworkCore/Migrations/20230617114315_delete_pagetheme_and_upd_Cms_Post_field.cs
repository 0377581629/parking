using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero.Migrations
{
    public partial class delete_pagetheme_and_upd_Cms_Post_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cms_Page_Layout_Cms_Page_Theme_PageThemeId",
                table: "Cms_Page_Layout");

            migrationBuilder.DropTable(
                name: "Cms_Widget_PageTheme");

            migrationBuilder.DropTable(
                name: "Cms_Page_Theme");

            migrationBuilder.DropIndex(
                name: "IX_Cms_Page_Layout_PageThemeId",
                table: "Cms_Page_Layout");

            migrationBuilder.DropColumn(
                name: "PageThemeId",
                table: "Cms_Page_Layout");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cms_Post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Cms_Post",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cms_Post");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Cms_Post");

            migrationBuilder.AddColumn<int>(
                name: "PageThemeId",
                table: "Cms_Page_Layout",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cms_Page_Theme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(95)", maxLength: 95, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numbering = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cms_Page_Theme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cms_Widget_PageTheme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageThemeId = table.Column<int>(type: "int", nullable: false),
                    WidgetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cms_Widget_PageTheme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cms_Widget_PageTheme_Cms_Page_Theme_PageThemeId",
                        column: x => x.PageThemeId,
                        principalTable: "Cms_Page_Theme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cms_Widget_PageTheme_Cms_Widget_WidgetId",
                        column: x => x.WidgetId,
                        principalTable: "Cms_Widget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cms_Page_Layout_PageThemeId",
                table: "Cms_Page_Layout",
                column: "PageThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cms_Widget_PageTheme_PageThemeId",
                table: "Cms_Widget_PageTheme",
                column: "PageThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cms_Widget_PageTheme_WidgetId",
                table: "Cms_Widget_PageTheme",
                column: "WidgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cms_Page_Layout_Cms_Page_Theme_PageThemeId",
                table: "Cms_Page_Layout",
                column: "PageThemeId",
                principalTable: "Cms_Page_Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

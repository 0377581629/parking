using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero.Migrations
{
    public partial class Update_Post_and_Category_and_Tag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cms_Post_Cms_Category_CategoryId",
                table: "Cms_Post");

            migrationBuilder.DropTable(
                name: "Cms_Post_Category_Detail");

            migrationBuilder.DropTable(
                name: "Cms_Post_Tag_Detail");

            migrationBuilder.DropTable(
                name: "Cms_Category");

            migrationBuilder.DropTable(
                name: "Cms_Tags");

            migrationBuilder.DropIndex(
                name: "IX_Cms_Post_CategoryId",
                table: "Cms_Post");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Cms_Post");

            migrationBuilder.DropColumn(
                name: "AuthorDefault",
                table: "Cms_Post");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Cms_Post");

            migrationBuilder.DropColumn(
                name: "CommentCount",
                table: "Cms_Post");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cms_Post");

            migrationBuilder.DropColumn(
                name: "DescriptionDefault",
                table: "Cms_Post");

            migrationBuilder.DropColumn(
                name: "Keyword",
                table: "Cms_Post");

            migrationBuilder.DropColumn(
                name: "KeywordDefault",
                table: "Cms_Post");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Cms_Post");

            migrationBuilder.DropColumn(
                name: "TitleDefault",
                table: "Cms_Post");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Cms_Post");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Cms_Post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AuthorDefault",
                table: "Cms_Post",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Cms_Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CommentCount",
                table: "Cms_Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cms_Post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DescriptionDefault",
                table: "Cms_Post",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Keyword",
                table: "Cms_Post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "KeywordDefault",
                table: "Cms_Post",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Cms_Post",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TitleDefault",
                table: "Cms_Post",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Cms_Post",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cms_Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorDefault = table.Column<bool>(type: "bit", nullable: false),
                    CategoryCode = table.Column<string>(type: "nvarchar(95)", maxLength: 95, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(95)", maxLength: 95, nullable: true),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionDefault = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeywordDefault = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numbering = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    PostCount = table.Column<int>(type: "int", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleDefault = table.Column<bool>(type: "bit", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cms_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cms_Category_Cms_Category_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Cms_Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cms_Tags",
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
                    Order = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    ViewCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cms_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cms_Post_Category_Detail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cms_Post_Category_Detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cms_Post_Category_Detail_Cms_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Cms_Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cms_Post_Category_Detail_Cms_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Cms_Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cms_Post_Tag_Detail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cms_Post_Tag_Detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cms_Post_Tag_Detail_Cms_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Cms_Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cms_Post_Tag_Detail_Cms_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Cms_Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cms_Post_CategoryId",
                table: "Cms_Post",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cms_Category_ParentId",
                table: "Cms_Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Cms_Post_Category_Detail_CategoryId",
                table: "Cms_Post_Category_Detail",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cms_Post_Category_Detail_PostId",
                table: "Cms_Post_Category_Detail",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Cms_Post_Tag_Detail_PostId",
                table: "Cms_Post_Tag_Detail",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Cms_Post_Tag_Detail_TagId",
                table: "Cms_Post_Tag_Detail",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cms_Post_Cms_Category_CategoryId",
                table: "Cms_Post",
                column: "CategoryId",
                principalTable: "Cms_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

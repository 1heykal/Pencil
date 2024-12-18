using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pencil.ContentManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyBoxToPostsRealtionship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Box_BoxId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_BoxId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "BoxId",
                table: "Post");

            migrationBuilder.CreateTable(
                name: "BoxPost",
                columns: table => new
                {
                    BoxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxPost", x => new { x.BoxId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_BoxPost_Box_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Box",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BoxPost_Post_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoxPost_PostsId",
                table: "BoxPost",
                column: "PostsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxPost");

            migrationBuilder.AddColumn<Guid>(
                name: "BoxId",
                table: "Post",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Post_BoxId",
                table: "Post",
                column: "BoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Box_BoxId",
                table: "Post",
                column: "BoxId",
                principalTable: "Box",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

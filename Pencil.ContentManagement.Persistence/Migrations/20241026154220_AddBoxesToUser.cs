using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pencil.ContentManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBoxesToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BoxId",
                table: "Post",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Box",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Private = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Box", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Box_ApplicationUser_CreatorId1",
                        column: x => x.CreatorId1,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_BoxId",
                table: "Post",
                column: "BoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Box_CreatorId1",
                table: "Box",
                column: "CreatorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Box_BoxId",
                table: "Post",
                column: "BoxId",
                principalTable: "Box",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Box_BoxId",
                table: "Post");

            migrationBuilder.DropTable(
                name: "Box");

            migrationBuilder.DropIndex(
                name: "IX_Post_BoxId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "BoxId",
                table: "Post");
        }
    }
}

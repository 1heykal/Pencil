using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pencil.ContentManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyBoxCreatorTypeToGuideAndRenamePrivatePropertyToPublic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Box_ApplicationUser_CreatorId1",
                table: "Box");

            migrationBuilder.DropIndex(
                name: "IX_Box_CreatorId1",
                table: "Box");

            migrationBuilder.DropColumn(
                name: "CreatorId1",
                table: "Box");

            migrationBuilder.RenameColumn(
                name: "Private",
                table: "Box",
                newName: "Public");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "Box",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Box_CreatorId",
                table: "Box",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Box_ApplicationUser_CreatorId",
                table: "Box",
                column: "CreatorId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Box_ApplicationUser_CreatorId",
                table: "Box");

            migrationBuilder.DropIndex(
                name: "IX_Box_CreatorId",
                table: "Box");

            migrationBuilder.RenameColumn(
                name: "Public",
                table: "Box",
                newName: "Private");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "Box",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId1",
                table: "Box",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Box_CreatorId1",
                table: "Box",
                column: "CreatorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Box_ApplicationUser_CreatorId1",
                table: "Box",
                column: "CreatorId1",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

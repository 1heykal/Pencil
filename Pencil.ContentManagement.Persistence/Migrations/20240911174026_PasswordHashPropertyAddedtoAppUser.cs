using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pencil.ContentManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PasswordHashPropertyAddedtoAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "ApplicationUser");
        }
    }
}

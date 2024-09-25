using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pencil.ContentManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedSoftDeletedToFollowing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SoftDeleted",
                table: "Following",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftDeleted",
                table: "Following");
        }
    }
}

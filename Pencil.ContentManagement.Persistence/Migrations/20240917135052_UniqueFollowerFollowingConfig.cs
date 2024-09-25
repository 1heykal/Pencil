using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pencil.ContentManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UniqueFollowerFollowingConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Following_FollowedId",
                table: "Following");

            migrationBuilder.CreateIndex(
                name: "IX_Following_FollowedId_FollowerId",
                table: "Following",
                columns: new[] { "FollowedId", "FollowerId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Following_FollowedId_FollowerId",
                table: "Following");

            migrationBuilder.CreateIndex(
                name: "IX_Following_FollowedId",
                table: "Following",
                column: "FollowedId");
        }
    }
}

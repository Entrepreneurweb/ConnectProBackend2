using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Social.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Social");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "Likes",
                newSchema: "Social");

            migrationBuilder.RenameTable(
                name: "Follows",
                newName: "Follows",
                newSchema: "Social");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Likes",
                schema: "Social",
                newName: "Likes");

            migrationBuilder.RenameTable(
                name: "Follows",
                schema: "Social",
                newName: "Follows");
        }
    }
}

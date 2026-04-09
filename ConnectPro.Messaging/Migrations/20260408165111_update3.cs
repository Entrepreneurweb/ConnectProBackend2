using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messaging.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Messaging");

            migrationBuilder.RenameTable(
                name: "ParticipantSnapshot",
                newName: "ParticipantSnapshot",
                newSchema: "Messaging");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Messages",
                newSchema: "Messaging");

            migrationBuilder.RenameTable(
                name: "Conversations",
                newName: "Conversations",
                newSchema: "Messaging");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ParticipantSnapshot",
                schema: "Messaging",
                newName: "ParticipantSnapshot");

            migrationBuilder.RenameTable(
                name: "Messages",
                schema: "Messaging",
                newName: "Messages");

            migrationBuilder.RenameTable(
                name: "Conversations",
                schema: "Messaging",
                newName: "Conversations");
        }
    }
}

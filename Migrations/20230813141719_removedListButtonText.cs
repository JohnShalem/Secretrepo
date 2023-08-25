using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WhatsAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class removedListButtonText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListButtonText",
                table: "PromptText");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ListButtonText",
                table: "PromptText",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

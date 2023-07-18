using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Data
{
    /// <inheritdoc />
    public partial class MRTMMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Users");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WealthApi.Migrations
{
    /// <inheritdoc />
    public partial class authorization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegistrationVerificationToken",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationVerificationToken",
                table: "Users");
        }
    }
}

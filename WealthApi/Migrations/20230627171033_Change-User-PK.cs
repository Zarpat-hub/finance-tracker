using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WealthApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsConfigurations_Users_Username",
                table: "AccountsConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistories_Users_Username",
                table: "TransactionHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Email");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsConfigurations_Users_Username",
                table: "AccountsConfigurations",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistories_Users_Username",
                table: "TransactionHistories",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountsConfigurations_Users_Username",
                table: "AccountsConfigurations");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistories_Users_Username",
                table: "TransactionHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountsConfigurations_Users_Username",
                table: "AccountsConfigurations",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistories_Users_Username",
                table: "TransactionHistories",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

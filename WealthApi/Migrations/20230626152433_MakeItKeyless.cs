using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WealthApi.Migrations
{
    /// <inheritdoc />
    public partial class MakeItKeyless : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionHistories",
                table: "TransactionHistories");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistories_Username",
                table: "TransactionHistories",
                column: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TransactionHistories_Username",
                table: "TransactionHistories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionHistories",
                table: "TransactionHistories",
                column: "Username");
        }
    }
}

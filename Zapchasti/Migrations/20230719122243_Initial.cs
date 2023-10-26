using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presentation.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PriceItems",
                table: "PriceItems");

            migrationBuilder.RenameTable(
                name: "PriceItems",
                newName: "postgresdb");

            migrationBuilder.AddPrimaryKey(
                name: "PK_postgresdb",
                table: "postgresdb",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_postgresdb",
                table: "postgresdb");

            migrationBuilder.RenameTable(
                name: "postgresdb",
                newName: "PriceItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PriceItems",
                table: "PriceItems",
                column: "Id");
        }
    }
}

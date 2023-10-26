using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presentation.Migrations
{
    /// <inheritdoc />
    public partial class initialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Бренд = table.Column<string>(type: "text", nullable: false),
                    Номерзапчасти = table.Column<string>(name: "Номер запчасти", type: "text", nullable: false),
                    Производительдляпоиска = table.Column<string>(name: "Производитель для поиска", type: "text", nullable: false),
                    Номердляпоиска = table.Column<string>(name: "Номер для поиска", type: "text", nullable: false),
                    Наименование = table.Column<string>(type: "text", nullable: false),
                    Цена = table.Column<decimal>(type: "numeric", nullable: false),
                    Количество = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceItems");
        }
    }
}

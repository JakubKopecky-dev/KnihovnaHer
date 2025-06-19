using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnihovnaHer.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewInicialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Zanry",
                keyColumn: "ZanrId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Zanry",
                keyColumn: "ZanrId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Zanry",
                keyColumn: "ZanrId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Zanry",
                keyColumn: "ZanrId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Zanry",
                keyColumn: "ZanrId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 3L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Vydavatele",
                columns: new[] { "VydavatelId", "Nazev" },
                values: new object[,]
                {
                    { 1L, "Epic Games" },
                    { 2L, "Bethesda" },
                    { 3L, "Electronic Arts" }
                });

            migrationBuilder.InsertData(
                table: "Zanry",
                columns: new[] { "ZanrId", "Nazev" },
                values: new object[,]
                {
                    { 1L, "Action" },
                    { 2L, "Adventure" },
                    { 3L, "RPG" },
                    { 4L, "Sci-Fi" },
                    { 5L, "Strategy" }
                });

            migrationBuilder.InsertData(
                table: "Hry",
                columns: new[] { "HraId", "Nazev", "RokVydani", "VydavatelId" },
                values: new object[,]
                {
                    { 1L, "The Witcher 3", 2015, 2L },
                    { 2L, "Fortnite", 2017, 1L },
                    { 3L, "Battlefield V", 2018, 3L }
                });
        }
    }
}

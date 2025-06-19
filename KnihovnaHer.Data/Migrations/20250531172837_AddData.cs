using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KnihovnaHer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Vydavatele",
                columns: new[] { "VydavatelId", "Nazev" },
                values: new object[,]
                {
                    { 1L, "Activision" },
                    { 2L, "Bandai Namco" },
                    { 3L, "Bethesda" },
                    { 4L, "Blizzard Entertainment" },
                    { 5L, "CD Projekt" },
                    { 6L, "Capcom" },
                    { 7L, "ConcernedApe" },
                    { 8L, "Electronic Arts" },
                    { 9L, "Epic Games" },
                    { 10L, "Larian Studios" },
                    { 11L, "Microsoft" },
                    { 12L, "Mojang" },
                    { 13L, "Nintendo" },
                    { 14L, "Paradox Interactive" },
                    { 15L, "Pocketpair" },
                    { 16L, "Rare" },
                    { 17L, "Re-Logic" },
                    { 18L, "Riot Games" },
                    { 19L, "Rockstar Games" },
                    { 20L, "Sony" },
                    { 21L, "Square Enix" },
                    { 22L, "Studio Wildcard" },
                    { 23L, "Supergiant Games" },
                    { 24L, "Team Cherry" },
                    { 25L, "Ubisoft" },
                    { 26L, "Valve" },
                    { 27L, "Warner Bros." }
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
                    { 5L, "Strategy" },
                    { 6L, "Fantasy" },
                    { 7L, "Shooter" },
                    { 8L, "Survival" },
                    { 9L, "Simulation" },
                    { 10L, "Horror" }
                });

            migrationBuilder.InsertData(
                table: "Hry",
                columns: new[] { "HraId", "Nazev", "RokVydani", "VydavatelId" },
                values: new object[,]
                {
                    { 1L, "Elden Ring", 2022, 2L },
                    { 2L, "Starfield", 2023, 3L },
                    { 3L, "Baldur's Gate 3", 2023, 10L },
                    { 4L, "Helldivers 2", 2024, 20L },
                    { 5L, "Palworld", 2024, 15L },
                    { 6L, "GTA V", 2013, 19L },
                    { 7L, "Cyberpunk 2077", 2020, 5L },
                    { 8L, "Hogwarts Legacy", 2023, 27L },
                    { 9L, "Assassin’s Creed Mirage", 2023, 25L },
                    { 10L, "Call of Duty: Modern Warfare II", 2022, 1L },
                    { 11L, "The Sims 4", 2014, 8L },
                    { 12L, "Resident Evil 4 Remake", 2023, 6L },
                    { 13L, "Stardew Valley", 2016, 7L },
                    { 14L, "Dead Space Remake", 2023, 8L },
                    { 15L, "Cities: Skylines II", 2023, 14L },
                    { 16L, "ARK: Survival Ascended", 2023, 22L },
                    { 17L, "Hades II", 2024, 23L },
                    { 18L, "The Witcher 3", 2015, 5L },
                    { 19L, "Fortnite", 2017, 9L },
                    { 20L, "Battlefield V", 2018, 8L },
                    { 21L, "Red Dead Redemption 2", 2018, 19L },
                    { 22L, "Overwatch 2", 2022, 4L },
                    { 23L, "Minecraft", 2011, 12L },
                    { 24L, "Valorant", 2020, 18L },
                    { 25L, "League of Legends", 2009, 18L },
                    { 26L, "Dota 2", 2013, 26L },
                    { 27L, "Apex Legends", 2019, 8L },
                    { 28L, "Sea of Thieves", 2018, 16L },
                    { 29L, "The Legend of Zelda: TOTK", 2023, 13L },
                    { 30L, "Super Mario Odyssey", 2017, 13L },
                    { 31L, "Hollow Knight: Silksong", 2025, 24L },
                    { 32L, "Diablo IV", 2023, 4L },
                    { 33L, "Final Fantasy XVI", 2023, 21L },
                    { 34L, "Ghost of Tsushima", 2020, 20L },
                    { 35L, "Death Stranding", 2019, 20L },
                    { 36L, "The Last of Us Part I", 2022, 20L },
                    { 37L, "Returnal", 2021, 20L },
                    { 38L, "Forza Horizon 5", 2021, 11L },
                    { 39L, "Microsoft Flight Simulator", 2020, 11L },
                    { 40L, "Terraria", 2011, 17L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "Hry",
                keyColumn: "HraId",
                keyValue: 40L);

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
                table: "Zanry",
                keyColumn: "ZanrId",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Zanry",
                keyColumn: "ZanrId",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Zanry",
                keyColumn: "ZanrId",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Zanry",
                keyColumn: "ZanrId",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Zanry",
                keyColumn: "ZanrId",
                keyValue: 10L);

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

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "Vydavatele",
                keyColumn: "VydavatelId",
                keyValue: 27L);
        }
    }
}

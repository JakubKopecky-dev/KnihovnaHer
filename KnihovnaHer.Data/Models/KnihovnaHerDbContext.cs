using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KnihovnaHer.Data.Models
{
    public class KnihovnaHerDbContext(DbContextOptions<KnihovnaHerDbContext>options) : IdentityDbContext<Uzivatel>(options)
    {


        public DbSet<Hra> Hry { get; set; }

        public DbSet<StatusHry> StatusHer { get; set; }

        public DbSet<Uzivatel> Uzivatele { get; set; }

        public DbSet<Vydavatel> Vydavatele { get; set; } 

        public DbSet<Zanr> Zanry { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
           AddTestingData(modelBuilder);


            //měním z cansade na restrict, takže nejde smazat parent dokud je pod ním child
            IEnumerable<IMutableForeignKey> cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(type => type.GetForeignKeys())
                .Where(foreignKey => !foreignKey.IsOwnership && foreignKey.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (IMutableForeignKey foreignKey in cascadeFKs)
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }



       
        private void AddTestingData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Zanr>().HasData(
                new Zanr { ZanrId = 1, Nazev = "Action" },
                new Zanr { ZanrId = 2, Nazev = "Adventure" },
                new Zanr { ZanrId = 3, Nazev = "RPG" },
                new Zanr { ZanrId = 4, Nazev = "Sci-Fi" },
                new Zanr { ZanrId = 5, Nazev = "Strategy" },
                new Zanr { ZanrId = 6, Nazev = "Fantasy" },
                new Zanr { ZanrId = 7, Nazev = "Shooter" },
                new Zanr { ZanrId = 8, Nazev = "Survival" },
                new Zanr { ZanrId = 9, Nazev = "Simulation" },
                new Zanr { ZanrId = 10, Nazev = "Horror" }
        
            );

            modelBuilder.Entity<Vydavatel>().HasData(
                new Vydavatel { VydavatelId = 1, Nazev = "Activision" },
                new Vydavatel { VydavatelId = 2, Nazev = "Bandai Namco" },
                new Vydavatel { VydavatelId = 3, Nazev = "Bethesda" },
                new Vydavatel { VydavatelId = 4, Nazev = "Blizzard Entertainment" },
                new Vydavatel { VydavatelId = 5, Nazev = "CD Projekt" },
                new Vydavatel { VydavatelId = 6, Nazev = "Capcom" },
                new Vydavatel { VydavatelId = 7, Nazev = "ConcernedApe" },
                new Vydavatel { VydavatelId = 8, Nazev = "Electronic Arts" },
                new Vydavatel { VydavatelId = 9, Nazev = "Epic Games" },
                new Vydavatel { VydavatelId = 10, Nazev = "Larian Studios" },
                new Vydavatel { VydavatelId = 11, Nazev = "Microsoft" },
                new Vydavatel { VydavatelId = 12, Nazev = "Mojang" },
                new Vydavatel { VydavatelId = 13, Nazev = "Nintendo" },
                new Vydavatel { VydavatelId = 14, Nazev = "Paradox Interactive" },
                new Vydavatel { VydavatelId = 15, Nazev = "Pocketpair" },
                new Vydavatel { VydavatelId = 16, Nazev = "Rare" },
                new Vydavatel { VydavatelId = 17, Nazev = "Re-Logic" },
                new Vydavatel { VydavatelId = 18, Nazev = "Riot Games" },
                new Vydavatel { VydavatelId = 19, Nazev = "Rockstar Games" },
                new Vydavatel { VydavatelId = 20, Nazev = "Sony" },
                new Vydavatel { VydavatelId = 21, Nazev = "Square Enix" },
                new Vydavatel { VydavatelId = 22, Nazev = "Studio Wildcard" },
                new Vydavatel { VydavatelId = 23, Nazev = "Supergiant Games" },
                new Vydavatel { VydavatelId = 24, Nazev = "Team Cherry" },
                new Vydavatel { VydavatelId = 25, Nazev = "Ubisoft" },
                new Vydavatel { VydavatelId = 26, Nazev = "Valve" },
                new Vydavatel { VydavatelId = 27, Nazev = "Warner Bros." }
        
            );

            modelBuilder.Entity<Hra>().HasData(
                new Hra { HraId = 1, Nazev = "Elden Ring", RokVydani = 2022, VydavatelId = 2 },
                new Hra { HraId = 2, Nazev = "Starfield", RokVydani = 2023, VydavatelId = 3 },
                new Hra { HraId = 3, Nazev = "Baldur's Gate 3", RokVydani = 2023, VydavatelId = 10 },
                new Hra { HraId = 4, Nazev = "Helldivers 2", RokVydani = 2024, VydavatelId = 20 },
                new Hra { HraId = 5, Nazev = "Palworld", RokVydani = 2024, VydavatelId = 15 },
                new Hra { HraId = 6, Nazev = "GTA V", RokVydani = 2013, VydavatelId = 19 },
                new Hra { HraId = 7, Nazev = "Cyberpunk 2077", RokVydani = 2020, VydavatelId = 5 },
                new Hra { HraId = 8, Nazev = "Hogwarts Legacy", RokVydani = 2023, VydavatelId = 27 },
                new Hra { HraId = 9, Nazev = "Assassin’s Creed Mirage", RokVydani = 2023, VydavatelId = 25 },
                new Hra { HraId = 10, Nazev = "Call of Duty: Modern Warfare II", RokVydani = 2022, VydavatelId = 1 },
                new Hra { HraId = 11, Nazev = "The Sims 4", RokVydani = 2014, VydavatelId = 8 },
                new Hra { HraId = 12, Nazev = "Resident Evil 4 Remake", RokVydani = 2023, VydavatelId = 6 },
                new Hra { HraId = 13, Nazev = "Stardew Valley", RokVydani = 2016, VydavatelId = 7 },
                new Hra { HraId = 14, Nazev = "Dead Space Remake", RokVydani = 2023, VydavatelId = 8 },
                new Hra { HraId = 15, Nazev = "Cities: Skylines II", RokVydani = 2023, VydavatelId = 14 },
                new Hra { HraId = 16, Nazev = "ARK: Survival Ascended", RokVydani = 2023, VydavatelId = 22 },
                new Hra { HraId = 17, Nazev = "Hades II", RokVydani = 2024, VydavatelId = 23 },
                new Hra { HraId = 18, Nazev = "The Witcher 3", RokVydani = 2015, VydavatelId = 5 },
                new Hra { HraId = 19, Nazev = "Fortnite", RokVydani = 2017, VydavatelId = 9 },
                new Hra { HraId = 20, Nazev = "Battlefield V", RokVydani = 2018, VydavatelId = 8 },
                new Hra { HraId = 21, Nazev = "Red Dead Redemption 2", RokVydani = 2018, VydavatelId = 19 },
                new Hra { HraId = 22, Nazev = "Overwatch 2", RokVydani = 2022, VydavatelId = 4 },
                new Hra { HraId = 23, Nazev = "Minecraft", RokVydani = 2011, VydavatelId = 12 },
                new Hra { HraId = 24, Nazev = "Valorant", RokVydani = 2020, VydavatelId = 18 },
                new Hra { HraId = 25, Nazev = "League of Legends", RokVydani = 2009, VydavatelId = 18 },
                new Hra { HraId = 26, Nazev = "Dota 2", RokVydani = 2013, VydavatelId = 26 },
                new Hra { HraId = 27, Nazev = "Apex Legends", RokVydani = 2019, VydavatelId = 8 },
                new Hra { HraId = 28, Nazev = "Sea of Thieves", RokVydani = 2018, VydavatelId = 16 },
                new Hra { HraId = 29, Nazev = "The Legend of Zelda: TOTK", RokVydani = 2023, VydavatelId = 13 },
                new Hra { HraId = 30, Nazev = "Super Mario Odyssey", RokVydani = 2017, VydavatelId = 13 },
                new Hra { HraId = 31, Nazev = "Hollow Knight: Silksong", RokVydani = 2025, VydavatelId = 24 },
                new Hra { HraId = 32, Nazev = "Diablo IV", RokVydani = 2023, VydavatelId = 4 },
                new Hra { HraId = 33, Nazev = "Final Fantasy XVI", RokVydani = 2023, VydavatelId = 21 },
                new Hra { HraId = 34, Nazev = "Ghost of Tsushima", RokVydani = 2020, VydavatelId = 20 },
                new Hra { HraId = 35, Nazev = "Death Stranding", RokVydani = 2019, VydavatelId = 20 },
                new Hra { HraId = 36, Nazev = "The Last of Us Part I", RokVydani = 2022, VydavatelId = 20 },
                new Hra { HraId = 37, Nazev = "Returnal", RokVydani = 2021, VydavatelId = 20 },
                new Hra { HraId = 38, Nazev = "Forza Horizon 5", RokVydani = 2021, VydavatelId = 11 },
                new Hra { HraId = 39, Nazev = "Microsoft Flight Simulator", RokVydani = 2020, VydavatelId = 11 },
                new Hra { HraId = 40, Nazev = "Terraria", RokVydani = 2011, VydavatelId = 17 }
        
            );
        }

        


    }

}


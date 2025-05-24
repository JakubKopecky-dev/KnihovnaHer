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
            // Žánry
            modelBuilder.Entity<Zanr>().HasData(
                new Zanr { ZanrId = 1, Nazev = "Action" },
                new Zanr { ZanrId = 2, Nazev = "Adventure" },
                new Zanr { ZanrId = 3, Nazev = "RPG" },
                new Zanr { ZanrId = 4, Nazev = "Sci-Fi" },
                new Zanr { ZanrId = 5, Nazev = "Strategy" }
            );

            // Vydavatelé
            modelBuilder.Entity<Vydavatel>().HasData(
                new Vydavatel { VydavatelId = 1, Nazev = "Epic Games" },
                new Vydavatel { VydavatelId = 2, Nazev = "Bethesda" },
                new Vydavatel { VydavatelId = 3, Nazev = "Electronic Arts" }
            );

            // Hry
            modelBuilder.Entity<Hra>().HasData(
                new Hra { HraId = 1, Nazev = "The Witcher 3", RokVydani = 2015, VydavatelId = 2 },
                new Hra { HraId = 2, Nazev = "Fortnite", RokVydani = 2017, VydavatelId = 1 },
                new Hra { HraId = 3, Nazev = "Battlefield V", RokVydani = 2018, VydavatelId = 3 }
            );

            
























        }

    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asztalfoglalas
{
    public class AsztalfoglalasContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=127.0.0.1;Database=Asztalfoglalas;Uid=root;Pwd=;", ServerVersion.AutoDetect("Server=127.0.0.1;Database=Asztalfoglalas;Uid=root;Pwd=;"));
        }

        public DbSet<Asztal> Asztal { get; set; }
        public DbSet<Foglalas> Foglalas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asztal>().HasData(
                new Asztal() { ID = 1, Megnevezes = "1-es asztal", Ferohely = 6 },
                new Asztal() { ID = 2, Megnevezes = "2-es asztal", Ferohely = 2 },
                new Asztal() { ID = 3, Megnevezes = "3-as asztal", Ferohely = 4 },
                new Asztal() { ID = 4, Megnevezes = "4-es asztal", Ferohely = 8 },
                new Asztal() { ID = 5, Megnevezes = "5-ös asztal", Ferohely = 4 }
            );

            modelBuilder.Entity<Foglalas>().HasData(
                new Foglalas() { ID = 1, AsztalID = 1, Datum = DateTime.Parse("2023.12.13"), Letszam = 4, Nev = "Teszt", Telefonszam = "06204568924" },
                new Foglalas() { ID = 2, AsztalID = 2, Datum = DateTime.Parse("2023.12.15"), Letszam = 2, Nev = "Teszt1", Telefonszam = "06204568954" }
            );
        }
    }
}

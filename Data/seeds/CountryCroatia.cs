using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Seeds
{
    internal static partial class Seeder
    {
        internal static ModelBuilder AddCroatia(this ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Country>()
                .HasData(new Country
                {
                    Id = 1,
                    Name = "Croatia",
                });

            modelBuilder
                .Entity<County>()
                .HasData(new List<County>
                {
                    new County
                    {
                        Id = 1,
                        Name = "Bjelovar-Bilogora",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 2,
                        Name = "Brod-Posavina",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 3,
                        Name = "Dubrovnik-Neretva",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 4,
                        Name = "Istria",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 5,
                        Name = "Karlovac",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 6,
                        Name = "Koprivnica-Krizevci",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 7,
                        Name = "Krapina-Zagorje",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 8,
                        Name = "Lika-Senj",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 9,
                        Name = "Medimurje",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 10,
                        Name = "Osijek-Baranja",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 11,
                        Name = "Pozega-Slavonia",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 12,
                        Name = "Primorje-Gorski Kotar",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 13,
                        Name = "Sisak-Moslavina",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 14,
                        Name = "Split-Dalmatia",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 15,
                        Name = "Sibenik-Knin",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 16,
                        Name = "Varazdin",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 17,
                        Name = "Virovitica-Podravina",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 18,
                        Name = "Vukovar-Srijem",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 19,
                        Name = "Zadar",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 20,
                        Name = "Zagreb",
                        CountryId = 1,
                    },
                    new County
                    {
                        Id = 21,
                        Name = "City of Zagreb",
                        CountryId = 1,
                    },
                });

            return modelBuilder;
        }
    }
}

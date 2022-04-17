using CountriesStructure.API.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CountriesStructure.API.Data
{
    public class CountryContext : DbContext
    {
        public CountryContext(DbContextOptions<CountryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasKey(k => k.Code);
            modelBuilder.Entity<Country>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Country>().Property(p => p.Code).IsRequired();
            modelBuilder.Entity<Country>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Country>().HasData(CountriesData.GetCountriesData());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Country> Countries { get; set; }   
    }
}

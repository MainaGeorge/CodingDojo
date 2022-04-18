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
        public DbSet<TopNeighbour> TopNeighbours { get; set; }  
        public DbSet<Country> Countries { get; set; }   
    }
}

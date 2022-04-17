using CountriesStructure.API.Data;
using CountriesStructure.API.Models;
using CountriesStructure.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CountriesStructure.API.Services.Implementations
{
    public class ContinentRepository : IContinentRepository
    {
        private readonly CountryContext _context;

        public ContinentRepository(CountryContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await _context.Countries.OrderBy(c => c.Id).ToArrayAsync();
        }

        public async Task<bool> ContainsCountry(string countryCode)
        {
            return await _context.Countries.AnyAsync(c =>
                c.Code.Equals(countryCode, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}

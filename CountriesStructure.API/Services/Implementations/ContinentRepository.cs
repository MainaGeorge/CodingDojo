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

        public async Task<IEnumerable<string>> GetPathFromOriginToDestination(string destinationCountryCode, string originCountryCode)
        {
            
            var destCountry = await _context.Countries.FirstOrDefaultAsync(c =>
                c.Code.Equals(destinationCountryCode, StringComparison.CurrentCultureIgnoreCase));

            var originCountry = await _context.Countries.FirstOrDefaultAsync(c =>
                c.Code.Equals(originCountryCode, StringComparison.CurrentCultureIgnoreCase));

            if (destCountry is null || originCountry is null)
                throw new ArgumentException("You can not travel to a country that does not exist");

            if (originCountry.Id > destCountry.Id)
                (originCountry, destCountry) = (destCountry, originCountry);

            return await _context.Countries.Where(c => c.Id >=originCountry.Id && c.Id <= destCountry.Id).Select(x => x.Code).ToListAsync();
        }
    }
}

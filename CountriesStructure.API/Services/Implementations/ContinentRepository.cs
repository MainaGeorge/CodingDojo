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

        public async Task<IEnumerable<string>> GetPathFromOriginToDestination(string destinationCountryCode, string originCountryCode)
        {
            if (string.IsNullOrWhiteSpace(destinationCountryCode) || string.IsNullOrWhiteSpace(originCountryCode))
                throw new ArgumentException("You have to enter both origin and destination countries");
            
            if(destinationCountryCode.Equals(originCountryCode, StringComparison.CurrentCultureIgnoreCase))
                throw new ArgumentException("You are already here dummy!!");

            var (originCountry, destCountry) = await GetDestinationAndOriginCountries(destinationCountryCode, originCountryCode);

            var countriesToPassThrough = new List<string>() {originCountry.Code};

            var topNeighbour = originCountry.TopNeighbour;

            topNeighbour = await TraverseCountriesBetweenOriginAndDestination(topNeighbour, destCountry.Code, originCountry.Code, countriesToPassThrough);

            if (topNeighbour is null)
                throw new Exception($"No such path exists from {originCountry.Code} to {destCountry.Code}");

            countriesToPassThrough.Add(destCountry.Code);
            return countriesToPassThrough;
        }
        
        public Task<IEnumerable<string>> GetPathToDestination(string destinationCountryCode) =>
            GetPathFromOriginToDestination(destinationCountryCode, "USA");
        
        private async Task<TopNeighbour?> TraverseCountriesBetweenOriginAndDestination(TopNeighbour? topNeighbour, string destinationCountryCode, 
            string originCountryCode, ICollection<string> countriesToPassThrough)
        {
            while (topNeighbour != null && !topNeighbour!.Code.Equals(destinationCountryCode))
            {
                var nextCountryToPassThrough =
                    await _context.Countries.Include(c => c.TopNeighbour)
                        .FirstOrDefaultAsync(c => c.Code.Equals(topNeighbour.Code));

                if (nextCountryToPassThrough?.TopNeighbour is null)
                    throw new Exception($"No such path exists from {originCountryCode} to {destinationCountryCode}");

                countriesToPassThrough.Add(topNeighbour.Code);
                topNeighbour = nextCountryToPassThrough.TopNeighbour;
            }

            return topNeighbour;
        }
        
        private async Task<(Country, Country)> GetDestinationAndOriginCountries(string destinationCountryCode, string originCountryCode)
        {
            var destCountry = await GetCountryWithTopNeighbour(destinationCountryCode);
            var originCountry = await GetCountryWithTopNeighbour(originCountryCode);
            
            if (destCountry is null || originCountry is null)
                throw new ArgumentException("You can not travel to or come from a country that does not exist");

            if (destCountry.Id > originCountry.Id)
                (originCountry, destCountry) = (destCountry, originCountry);

            return (originCountry, destCountry);
        }
        
        private async Task<Country?> GetCountryWithTopNeighbour(string countryCode) => await _context
            .Countries
            .Include(c => c.TopNeighbour)
            .FirstOrDefaultAsync(c => c.Code.Equals(countryCode, StringComparison.CurrentCultureIgnoreCase)); 
    }
}

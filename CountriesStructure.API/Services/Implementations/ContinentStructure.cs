using CountriesStructure.API.Data;
using CountriesStructure.API.Models;
using CountriesStructure.API.Services.Interfaces;
using CountriesStructure.Library;

namespace CountriesStructure.API.Services.Implementations
#nullable disable
{
    public class ContinentStructure : IContinentStructure
    {
        private readonly ContinentNode _continent;
        private readonly IEnumerable<Country> _countries;

        public ContinentStructure()
        {
            _continent = new ContinentNode();
            _countries = CountriesData.GetCountriesData();
            foreach (var country in _countries)
                _continent.AddCountry(country.Code, country.TopNeighbourCode);
        }

        
        public async Task<IEnumerable<string>> GetPathFromOriginToDestination(string destinationCountryCode, string originCountryCode)
        {
            if (!_countries.Any(c => c.Code.Equals(destinationCountryCode, StringComparison.CurrentCultureIgnoreCase)))
                throw new ArgumentException("You can not travel to a country that does not exist");

            return await Task.FromResult(_continent.GetPathFromOriginToDestination(destinationCountryCode, originCountryCode));
        }
    }
}

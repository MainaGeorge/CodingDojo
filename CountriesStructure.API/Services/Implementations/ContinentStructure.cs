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
            var destCountry = _countries.FirstOrDefault(c =>
                c.Code.Equals(destinationCountryCode, StringComparison.CurrentCultureIgnoreCase));

            var originCountry = _countries.FirstOrDefault(c =>
                c.Code.Equals(originCountryCode, StringComparison.CurrentCultureIgnoreCase));

            if(destCountry is null || originCountry is null)
                throw new ArgumentException("You can not travel to a country that does not exist");

            //traversal is downwards only, so if the destination is higher than origin, we have to switch them
            if (originCountry.Id > destCountry.Id)
                (destinationCountryCode, originCountryCode) = (originCountryCode, destinationCountryCode);

            return await Task.FromResult(_continent.GetPathFromOriginToDestination(destinationCountryCode, originCountryCode));
        }
    }
}

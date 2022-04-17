using CountriesStructure.API.Models;

namespace CountriesStructure.API.Services.Interfaces
{
    public interface IContinentRepository
    {
        Task<IEnumerable<Country>> GetCountries();
        Task<bool> ContainsCountry(string countryCode);
    }
}

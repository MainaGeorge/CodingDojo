namespace CountriesStructure.API.Services.Interfaces
{
    public interface IContinentRepository
    {
        Task<IEnumerable<string>> GetPathFromOriginToDestination(string destinationCountryCode, string originCountryCode);
        Task<IEnumerable<string>> GetPathToDestination(string destinationCountryCode);

    }
}

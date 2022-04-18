namespace CountriesStructure.API.Services.Interfaces
{
    public interface IContinentStructure
    {
        Task<IEnumerable<string>> GetPathFromOriginToDestination(string destinationCountryCode, string originCountryCode);
        Task<IEnumerable<string>> GetPathToDestination(string destinationCountryCode);

    }
}

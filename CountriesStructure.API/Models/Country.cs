namespace CountriesStructure.API.Models
#nullable disable
{
    public record Country(string Name, string Code, string TopNeighbourCode, int Id);
}

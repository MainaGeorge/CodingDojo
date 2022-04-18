using CountriesStructure.API.Models;

namespace CountriesStructure.API.Data
{
    public class CountriesData
    {
        public static IEnumerable<Country> GetCountriesData() =>
            new List<Country>()
            {
                new(){Name = "Canada", Code = "CAN", TopNeighbourCode = ""},
                new(){Name="United States of America", Code = "USA", TopNeighbourCode = "CAN"},
                new(){Name="Mexico", Code = "MEX", TopNeighbourCode = "USA"},
                new(){Name="Belize", Code = "BLZ", TopNeighbourCode = "MEX"},
                new(){Name = "Guatemala", Code = "GTM", TopNeighbourCode = "MEX"},
                new(){Name = "El Salvador", Code = "SLV", TopNeighbourCode = "GTM"},
                new(){Name = "Honduras", Code = "HND", TopNeighbourCode = "GTM"},
                new(){Name = "Nicaragua", Code = "NIC", TopNeighbourCode = "HND"},
                new(){Name = "CostaRica",Code = "CRI", TopNeighbourCode = "NIC"},
                new(){Name = "Panama", Code = "PAN", TopNeighbourCode = "CRI"}
            };

        public static IEnumerable<TopNeighbour> GetTopNeighboursData() =>
            new List<TopNeighbour>()
            {
                new(){Code = "CAN"},
                new(){Code = "USA"},
                new(){Code = "MEX"},
                new(){Code = "GTM"},
                new(){Code = "HND"},
                new(){Code = "NIC"},
                new(){Code = "CRI"},
            };
        public static void SeedCountriesInMemoryData(CountryContext context)
        {
            if (context.Countries.Any()) return;

            context.TopNeighbours.AddRange(GetTopNeighboursData());
            context.Countries.AddRange(GetCountriesData());
            context.SaveChanges();
        }
    }
}

using CountriesStructure.API.Models;

namespace CountriesStructure.API.Data
{
    public class CountriesData
    {
        public static IEnumerable<Country> GetCountriesData() =>
            new List<Country>()
            {
                new(){Name = "Canada", Code = "CAN", TopNeighbourCode = "",Id = 1},
                new(){Name="United States of America", Code = "USA", TopNeighbourCode = "CAN",Id=2},
                new(){Name="Mexico", Code = "MEX", TopNeighbourCode = "USA", Id=3},
                new(){Name="Belize", Code = "BLZ", TopNeighbourCode = "MEX", Id=4},
                new(){Name = "Guatemala", Code = "GTM", TopNeighbourCode = "MEX", Id=5},
                new(){Name = "El Salvador", Code = "SLV", TopNeighbourCode = "GTM", Id=6},
                new(){Name = "Honduras", Code = "HND", TopNeighbourCode = "GTM", Id=7},
                new(){Name = "Nicaragua", Code = "NIC", TopNeighbourCode = "HND", Id=8},
                new(){Name = "CostaRica",Code = "CRI", TopNeighbourCode = "NIC", Id=9},
                new(){Name = "Panama", Code = "PAN", TopNeighbourCode = "CRI", Id=10}
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
        public static async Task SeedCountriesInMemoryData(CountryContext context)
        {
            if (context.Countries.Any()) return;

            await context.TopNeighbours.AddRangeAsync(GetTopNeighboursData());
            await context.Countries.AddRangeAsync(GetCountriesData());
            await context.SaveChangesAsync();
        }
    }
}

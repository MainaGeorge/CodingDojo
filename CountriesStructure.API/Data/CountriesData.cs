using CountriesStructure.API.Models;

namespace CountriesStructure.API.Data
{
    public class CountriesData
    {
        public static IEnumerable<Country> GetCountriesData() =>
            new List<Country>()
            {
                new("Canada", "CAN", 1),
                new("United States of America", "USA", 2),
                new("Mexico", "MEX", 3),
                new("Belize", "BLZ", 4),
                new("Guatemala", "GTM", 5),
                new("El Salvador", "SLV", 6),
                new("Honduras", "HND", 7),
                new("Nicaragua", "NIC", 8),
                new("CostaRica", "CRI", 9),
                new("Panama", "PAN", 10)
            };

        public static void SeedCountriesInMemoryData(CountryContext context)
        {
            if (context.Countries.Any()) return;

            context.Countries.AddRange(GetCountriesData());
            context.SaveChanges();
        }
    }
}

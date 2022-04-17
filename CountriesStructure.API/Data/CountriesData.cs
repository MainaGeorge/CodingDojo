using CountriesStructure.API.Models;

namespace CountriesStructure.API.Data
{
    public class CountriesData
    {
        public static IEnumerable<Country> GetCountriesData() =>
            new List<Country>()
            {
                new("Canada", "CAN", "", 1),
                new("United States of America", "USA", "CAN", 2),
                new("Mexico", "MEX", "USA", 3),
                new("Belize", "BLZ", "MEX", 4),
                new("Guatemala", "GTM", "MEX", 5),
                new("El Salvador", "SLV", "GTM", 6),
                new("Honduras", "HND", "GTM", 7),
                new("Nicaragua", "NIC", "HND", 8),
                new("CostaRica", "CRI", "NIC", 9),
                new("Panama", "PAN", "CRI", 10)
            };

        public static void SeedCountriesInMemoryData(CountryContext context)
        {
            if (context.Countries.Any()) return;

            context.Countries.AddRange(GetCountriesData());
            context.SaveChanges();
        }
    }
}

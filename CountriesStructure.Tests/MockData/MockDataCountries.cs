using System.Collections.Generic;
using CountriesStructure.API.Models;

namespace CountriesStructure.Tests.MockData
{
    public class MockDataCountries
    {
        public static IEnumerable<Country> GetCountries() => new List<Country>()
        {
            new() {Name = "Canada", Code = "CAN", TopNeighbourCode = "", Id = 1},
            new() {Name = "United States of America", Code = "USA", TopNeighbourCode = "CAN", Id = 2},
            new() {Name = "Mexico", Code = "MEX", TopNeighbourCode = "USA", Id = 3},
            new() {Name = "Guatemala", Code = "GTM", TopNeighbourCode = "MEX", Id = 4},
            new() {Name="Belize", Code = "BLZ", TopNeighbourCode = "MEX", Id=5},

        };


        public static IEnumerable<TopNeighbour> GetTopNeighbours() => new List<TopNeighbour>()
        {
            new() {Code = "CAN"},
            new() {Code = "USA"},
            new() {Code = "MEX"},
            new() {Code = "GTM"}
        };
    }
}

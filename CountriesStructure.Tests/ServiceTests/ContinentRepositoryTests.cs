using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountriesStructure.API.Data;
using CountriesStructure.API.Services.Implementations;
using CountriesStructure.Tests.MockData;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CountriesStructure.Tests.ServiceTests
{
    public class ContinentRepositoryTests : IDisposable
    {
        private readonly CountryContext _context;
        private readonly ContinentRepository _repo;
        public ContinentRepositoryTests()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<CountryContext>();
            dbContextOptionsBuilder.UseInMemoryDatabase(new Guid().ToString());

            _context = new CountryContext(dbContextOptionsBuilder.Options);
            _context.Database.EnsureCreated(); 
            _context.Countries.AddRange(MockDataCountries.GetCountries());
            _context.TopNeighbours.AddRange(MockDataCountries.GetTopNeighbours());

            _context.SaveChanges();

            _repo = new ContinentRepository(_context);
        }

        [Fact]
        public async Task ContextShouldContain_CorrectNumberOfCountriesAndTopCountries()
        {
            var numberOfCountries = await _context.Countries.CountAsync();
            var numberOfTopNeighbours = await _context.TopNeighbours.CountAsync();


            Assert.Equal(MockDataCountries.GetCountries().Count(), numberOfCountries);
            Assert.Equal(MockDataCountries.GetTopNeighbours().Count(), numberOfTopNeighbours);
        }

        [Theory]
        [InlineData("MEX", "USA", new[] { "MEX", "USA" })]
        [InlineData("GTM", "CAN", new[] { "GTM", "MEX", "USA", "CAN" })]
        public async Task Service_ReturnCorrectPath_WhenUsedWithValidContinentMembers(string destinationCountryCode, string originCountryCode, 
            IEnumerable<string> expectedPath)
        {
        
            var path = await _repo.GetPathFromOriginToDestination(destinationCountryCode, originCountryCode);
        
            Assert.Equal(expectedPath, path);
        }

        [Theory]
        [InlineData("PAN", "SLV")]
        public async Task RepoThrowsArgumentException_ifOneOfTheCountriesDoesNotExist(string origin, string destination)
        {
            await Assert.ThrowsAsync<ArgumentException>(() =>  _repo.GetPathFromOriginToDestination(destination, origin));
        }

        [Theory]
        [InlineData("BLZ", "GTM")]
        public async Task Continent_ThrowsException_ifNoPathBetweenTwoCountriesExist(string origin, string destination)
        {
            await Assert.ThrowsAsync<Exception>(() => _repo.GetPathFromOriginToDestination(destination, origin));
        }


        public void Dispose()
        {
            _context.Database.EnsureDeleted();

            _context.Dispose();
        }
    }
}

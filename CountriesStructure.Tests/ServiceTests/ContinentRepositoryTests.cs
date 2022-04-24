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
            var error = await Assert.ThrowsAsync<ArgumentException>(() =>  _repo.GetPathFromOriginToDestination(destination, origin));
            Assert.Equal("You can not travel to or come from a country that does not exist", error.Message);
        }

        [Theory]
        [InlineData("BLZ", "GTM")]
        public async Task Continent_ThrowsException_ifNoPathBetweenTwoCountriesExist(string origin, string destination)
        {
            var error = await Assert.ThrowsAsync<Exception>(() => _repo.GetPathFromOriginToDestination(destination, origin));
            Assert.Equal($"No such path exists from {origin} to {destination}", error.Message);
        }

        [Theory]
        [InlineData("", "MEX")]
        [InlineData( "MEX", "")]
        [InlineData(null, "MEX")]
        [InlineData("GTM", null)]      
        [InlineData(null, null)]
        public async Task Continent_ThrowsArgumentException_ifOneOrBothDestinationAndOriginCodesIsNullOrEmpty(
            string destination, string origin)
        {
            var error = await Assert.ThrowsAsync<ArgumentException>(() =>
                _repo.GetPathFromOriginToDestination(destination, origin));
            
            Assert.Equal("You have to enter both origin and destination countries", error.Message);
        }

        [Theory]
        [InlineData("MEX", "mex")]
        [InlineData("USA", "uSa")]
        [InlineData("CAN", "CAN")]
        public async Task Continent_ThrowsArgumentException_IfOriginAndDestinationCodesAreTheSame(string origin,
            string destination)
        {
            var error = await Assert.ThrowsAsync<ArgumentException>(() =>
                _repo.GetPathFromOriginToDestination(origin, destination));
            
            Assert.Equal("You are already here dummy!!", error.Message);
        }
        

        public void Dispose()
        {
            _context.Database.EnsureDeleted();

            _context.Dispose();
        }
    }
}

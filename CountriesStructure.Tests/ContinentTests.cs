using System;
using System.Collections.Generic;
using CountriesStructure.Library;
using Xunit;

namespace CountriesStructure.Tests
{
    public class ContinentTests
    {
        private readonly ContinentNode _continent;

        
        public  ContinentTests()
        {
            _continent = ContinentData.GetContinentTestData();
        }

        [Fact]
        public void ContinentAddsCountry_Correctly_GivenAValidTopNeighbour()
        {
            _continent.AddCountry("BLZ", "MEX");

            var blz = _continent.FindCountryNodeWithGivenCountryCode("BLZ");

            Assert.Equal("MEX", blz.TopNeighbour.Code);
        }

        [Fact]
        public void ContinentThrowsError_AddingCountry_WithInvalidTopNeighbour()
        {
           Assert.Throws<ArgumentException>( () => _continent.AddCountry("BLZ", "NIC"));
        }

        [Fact]
        public void ContinentThrowsError_AddingCountry_WithItselfAsTheTopNeighbour()
        {
            Assert.Throws<ArgumentException>(() => _continent.AddCountry("BLZ", "BLZ"));
            Assert.Throws<ArgumentException>(() => _continent.AddCountry("BLZ", "blz"));
        }

        [Fact]
        public void ContinentThrowsError_AddingCountry_WithNoTopNeighbour()
        {
            Assert.Throws<ArgumentException>(() => _continent.AddCountry("BLZ", ""));
            Assert.Throws<ArgumentException>(() => _continent.AddCountry("BLZ", null));
        }

        [Theory]
        [InlineData("MEX", "MEX")]
        [InlineData("mex", "MEX")]
        [InlineData("USA", "USA")]
        [InlineData("cAN", "CAN")]
        public void ContinentCorrectlyFindsCountry_GivenAValidCode(string countryCode, string expectedResult)
        {
            var countryNode = _continent.FindCountryNodeWithGivenCountryCode(countryCode);

            Assert.Equal(expectedResult, countryNode.Code);
        }

        
        [Theory]
        [InlineData("NIC")]
        [InlineData("PAN")]
        public void ContinentReturnsNull_IfNoCountry_ContainsGivenCode(string countryCode)
        {
            Assert.Null(_continent.FindCountryNodeWithGivenCountryCode(countryCode));
        }

        [Theory]
        [InlineData("MEX", "USA", new[]{"MEX", "USA" })]
        [InlineData("GUA", "CAN", new[]{"GUA", "MEX", "USA", "CAN"})]
        public void ContinentCorrectly_CalculatesPath_BetweenTwoCountries(string destination, string origin, IEnumerable<string> path)
        {
            var expected =_continent.GetPathFromOriginToDestination(destination, origin);
            Assert.Equal(path, expected);
        }

        [Theory]
        [InlineData("MEX")]
        [InlineData("GUA")]
        public void CountryOfOrigin_DefaultsToUsa_IfNotSpecified(string destination)
        {
            var path = _continent.GetPathFromOriginToDestination(destination);

            Assert.Contains("USA", path);
        }
    }
}
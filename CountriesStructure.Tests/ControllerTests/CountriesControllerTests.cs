using System;
using System.Threading.Tasks;
using CountriesStructure.API.Controllers;
using CountriesStructure.API.Models;
using CountriesStructure.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CountriesStructure.Tests.ControllerTests
{
    public class CountriesControllerTests
    {
        private readonly CountriesController _countriesController;
        private const string DESTINATION_COUNTRY_CODE = "MEX";
        private const string ORIGIN_COUNTRY_CODE = "USA";

        public CountriesControllerTests()
        {
            var mockRepo = new Mock<IContinentRepository>();
            var mockStructure = new Mock<IContinentStructure>();

            mockRepo.Setup(_ => _.GetPathFromOriginToDestination(DESTINATION_COUNTRY_CODE, ORIGIN_COUNTRY_CODE))
                .ReturnsAsync(new[] { "USA", "MEX" });
            mockRepo.Setup(_ => _.GetPathToDestination(DESTINATION_COUNTRY_CODE)).ReturnsAsync(new[] { "USA", "MEX" });

            mockStructure.Setup(_ => _.GetPathFromOriginToDestination(DESTINATION_COUNTRY_CODE, ORIGIN_COUNTRY_CODE))
                .ReturnsAsync(new[] { "USA", "MEX" });
            mockStructure.Setup(_ => _.GetPathToDestination(DESTINATION_COUNTRY_CODE))
                .ReturnsAsync(new[] { "USA", "MEX" });

            _countriesController = new CountriesController(mockStructure.Object, mockRepo.Object);
        }

        [Theory]
        [InlineData(DESTINATION_COUNTRY_CODE, ORIGIN_COUNTRY_CODE)]
        public async Task Controller_Returns200Ok(string destinationCountryCode, string originCountryCode)
        {
            var resultOne = await _countriesController.GetCountriesFromSourceToDestination(destinationCountryCode,originCountryCode);
            var resultTwo = await _countriesController.GetCountriesToDestination(destinationCountryCode);

            Assert.IsType<OkObjectResult>(resultOne.Result);
            Assert.IsType<OkObjectResult>(resultTwo.Result);

        }

        [Theory]
        [InlineData(DESTINATION_COUNTRY_CODE, ORIGIN_COUNTRY_CODE)]
        public async Task Controller_ReturnsResponseDataType(string destinationCountryCode, string originCountryCode)
        {
            var result = await _countriesController.GetCountriesFromSourceToDestination(destinationCountryCode, originCountryCode);
            var resultTwo = await _countriesController.GetCountriesToDestination(destinationCountryCode);

            var resultType = result.Result as OkObjectResult;
            var resultTwoType = resultTwo.Result as OkObjectResult;

            Assert.IsType<ResponseData>(resultType?.Value);
            Assert.IsType<ResponseData>(resultTwoType?.Value);
        }

        [Theory]
        [InlineData(DESTINATION_COUNTRY_CODE, ORIGIN_COUNTRY_CODE)]
        public async Task Controller_ReturnsCorrectResult(string destinationCountryCode, string originCountryCode)
        {
            
            var result = await _countriesController.GetCountriesFromSourceToDestination(destinationCountryCode, originCountryCode);
            var resultTwo = await _countriesController.GetCountriesToDestination(destinationCountryCode);

            var resultType = result.Result as OkObjectResult;
            var resultTwoType = resultTwo.Result as OkObjectResult;

            var actualResultOne = resultType?.Value as ResponseData;
            var actualResultTwo = resultTwoType?.Value as ResponseData;

            Assert.Equal(new[]{"USA", "MEX"}, actualResultOne?.PathFromDataStructure);
            Assert.Equal(new[]{"USA", "MEX"}, actualResultOne?.PathFromDatabase);

            Assert.Equal(new[]{"USA", "MEX"}, actualResultTwo?.PathFromDatabase);
            Assert.Equal(new[]{"USA", "MEX"}, actualResultTwo?.PathFromDataStructure);

        }
    }
}

using CountriesStructure.API.Models;
using CountriesStructure.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CountriesStructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IContinentStructure _continentStructure;
        private readonly IContinentRepository _continentRepo;

        public CountriesController(IContinentStructure continentStructure, IContinentRepository continentRepo)
        {
            _continentStructure = continentStructure;
            _continentRepo = continentRepo;
        }

        [HttpGet("{destinationCountryCode}")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries(string destinationCountryCode, string originCountryCode="USA")
        {
            var structureResult =
                await _continentStructure.GetPathFromOriginToDestination(destinationCountryCode, originCountryCode);
            var databaseResult =
                await _continentRepo.GetPathFromOriginToDestination(destinationCountryCode, originCountryCode);

            return Ok(new {structureResult, databaseResult});
        }
    }
}

using CountriesStructure.API.Models;
using CountriesStructure.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CountriesStructure.API.Controllers
{
    [Route("path")]
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
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries(string destinationCountryCode)
        {
            var structureResult =
                await _continentStructure.GetPathFromOriginToDestination(destinationCountryCode, "USA");
            var databaseResult =
                await _continentRepo.GetPathFromOriginToDestination(destinationCountryCode, "USA");
            
            return Ok(new
            {
                structureResult,
                databaseResult,
            });
        }

        [HttpGet("{source}/{destination}")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountriesFromSourceToDestination(string source,
            string destination)
        {
            var structureResult =
            await _continentStructure.GetPathFromOriginToDestination(source, destination);
            var databaseResult =
                await _continentRepo.GetPathFromOriginToDestination(source, destination);

            return Ok(new
            {
                structureResult,
                databaseResult,
            });

        }
    }
}

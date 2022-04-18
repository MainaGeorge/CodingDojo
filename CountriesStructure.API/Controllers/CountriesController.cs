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
                await _continentStructure.GetPathFromOriginToDestination(destinationCountryCode, "Usa");
            var databaseResult =
                await _continentRepo.GetPathFromOriginToDestination(destinationCountryCode, "usa");
            
            return Ok(new
            {
                structureResult,
                databaseResult,
            });
        }
    }
}

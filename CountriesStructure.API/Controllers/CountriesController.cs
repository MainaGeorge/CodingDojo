using CountriesStructure.API.Data;
using CountriesStructure.API.Models;
using CountriesStructure.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CountriesStructure.API.Controllers
{
    [Route("path")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IContinentStructure _continentStructure;
        private readonly IContinentRepository _continentRepo;
        private readonly CountryContext _context;

        public CountriesController(IContinentStructure continentStructure, IContinentRepository continentRepo, CountryContext context)
        {
            _continentStructure = continentStructure;
            _continentRepo = continentRepo;
            _context = context;
        }

        
        [HttpGet("{destinationCountryCode}")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries(string destinationCountryCode)
        {
            var structureResult =
                await _continentStructure.GetPathFromOriginToDestination(destinationCountryCode, "Usa");
            var databaseResult =
                await _continentRepo.GetPathFromOriginToDestination(destinationCountryCode, "usa");
            
            // var countriesToPassThrough = await _continentRepo.GetPathFromOriginToDestination("BLZ", "USA");
            var countries = await _context.Countries.Include(c => c.TopNeighbour).ToListAsync();
            return Ok(new
            {
                structureResult,
                databaseResult,
                countries
            });
        }
    }
}

using CountriesStructure.API.Models;
using CountriesStructure.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CountriesStructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IContinentStructure _continent;

        public CountriesController(IContinentStructure continent)
        {
            _continent = continent;
        }

        [HttpGet("{destinationCountryCode}")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries(string destinationCountryCode, string originCountryCode="USA")
        {
            return Ok(await  _continent.GetPathFromOriginToDestination(destinationCountryCode, originCountryCode));
        }
    }
}

using CountriesStructure.API.Models;
using CountriesStructure.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CountriesStructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IContinentRepository _repo;

        public CountriesController(IContinentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return Ok(await  _repo.GetCountries());
        }
    }
}

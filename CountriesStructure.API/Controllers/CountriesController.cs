using CountriesStructure.API.Data;
using CountriesStructure.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CountriesStructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly CountryContext _context;

        public CountriesController(CountryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }
    }
}

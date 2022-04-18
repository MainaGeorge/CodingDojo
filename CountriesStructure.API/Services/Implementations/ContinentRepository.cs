﻿using CountriesStructure.API.Data;
using CountriesStructure.API.Models;
using CountriesStructure.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CountriesStructure.API.Services.Implementations
{
    public class ContinentRepository : IContinentRepository
    {
        private readonly CountryContext _context;

        public ContinentRepository(CountryContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Country>> GetCountries()
        {
            return await _context.Countries.ToArrayAsync();
        }

        public async Task<bool> ContainsCountry(string countryCode)
        {
            return await _context.Countries.AnyAsync(c =>
                c.Code.Equals(countryCode, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<IEnumerable<string>> GetPathFromOriginToDestination(string destinationCountryCode, string originCountryCode)
        {
            if(destinationCountryCode.Equals(originCountryCode, StringComparison.CurrentCultureIgnoreCase))
                throw new Exception("You are already here dummy!!");

            var (originCountry, destCountry) = await GetDestinationAndOriginCountries(destinationCountryCode, originCountryCode);

            var countriesToPassThrough = new List<string>() {originCountry.Code};

            var topNeighbour = originCountry.TopNeighbour;

            while (topNeighbour != null && !topNeighbour!.Code.Equals(destCountry.Code))
            {
                var nextTopNeighbour =
                    await _context.Countries.Include(c => c.TopNeighbour).FirstOrDefaultAsync(c => c.Code.Equals(topNeighbour.Code));

                if (nextTopNeighbour?.TopNeighbour is null)
                    throw new Exception($"No such path exists from {originCountry.Code} to {destCountry.Code}");

                countriesToPassThrough.Add(topNeighbour.Code);
                topNeighbour = nextTopNeighbour.TopNeighbour;
            }

            if (topNeighbour is null)
                throw new Exception($"No such path exists from {originCountry.Code} to {destCountry.Code}");

            countriesToPassThrough.Add(destCountry.Code);
            return countriesToPassThrough;

        }

        private async Task<(Country, Country)> GetDestinationAndOriginCountries(string destinationCountryCode, string originCountryCode)
        {
            var destCountry = await _context.Countries.Include(c => c.TopNeighbour).FirstOrDefaultAsync(c =>
                c.Code.Equals(destinationCountryCode, StringComparison.CurrentCultureIgnoreCase));

            var originCountry = await _context.Countries.Include(c => c.TopNeighbour).FirstOrDefaultAsync(c =>
                c.Code.Equals(originCountryCode, StringComparison.CurrentCultureIgnoreCase));

            if (destCountry is null || originCountry is null)
                throw new ArgumentException("You can not travel to or come from a country that does not exist");

            if (destCountry.Id > originCountry.Id)
                (originCountry, destCountry) = (destCountry, originCountry);

            return (originCountry, destCountry);
        }
    }
}

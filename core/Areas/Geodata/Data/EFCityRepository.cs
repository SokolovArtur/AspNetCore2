using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tochka.Data;

namespace Tochka.Areas.Geodata.Data
{
    public class EFCityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public EFCityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<City> Cities => _context.Cities;

        public IEnumerable<SelectListItem> CitiesInSelectList(IEnumerable<City> cities)
        {
            return cities
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();
        }

        public async Task<IEnumerable<City>> RepresentationCities()
        {
            IEnumerable<City> cities = await _context.Cities
                .Where(c => c.IsRepresentation == true)
                .ToListAsync();
            return cities;
        }
    }
}

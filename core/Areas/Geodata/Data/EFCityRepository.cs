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

        public IQueryable<City> Cities => _context.Cities.AsNoTracking();

        public IEnumerable<SelectListItem> SelectListCities(IEnumerable<City> cities)
        {
            return cities
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();
        }

        public async Task<IEnumerable<City>> RepresentationCitiesAsync()
        {
            IEnumerable<City> cities = await Cities
                .Where(c => c.IsRepresentation == true)
                .ToListAsync();
            return cities;
        }
    }
}

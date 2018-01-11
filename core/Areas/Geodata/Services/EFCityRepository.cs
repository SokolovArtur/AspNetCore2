using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tochka.Areas.Geodata.Data;
using Tochka.Data;

namespace Tochka.Areas.Geodata.Services
{
    public class EFCityRepository : ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public EFCityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<City> Cities => _context.Cities.AsNoTracking();

        public IQueryable<City> GetRepresentationCities()
        {
            return Cities
                .Where(c => c.IsRepresentation == true);
        }
    
        public IEnumerable<SelectListItem> SelectListCities(IEnumerable<City> cities)
        {
            return cities
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();
        }
    }
}

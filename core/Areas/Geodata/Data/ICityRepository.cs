using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tochka.Areas.Geodata.Data
{
    public interface ICityRepository
    {
        IQueryable<City> Cities { get; }

        IQueryable<City> GetRepresentationCities();
        
        IEnumerable<SelectListItem> SelectListCities(IEnumerable<City> cities);
    }
}

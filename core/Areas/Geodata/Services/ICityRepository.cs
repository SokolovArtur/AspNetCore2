using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tochka.Areas.Geodata.Data;

namespace Tochka.Areas.Geodata.Services
{
    public interface ICityRepository
    {
        IQueryable<City> Cities { get; }

        IQueryable<City> GetRepresentationCities();
        
        IEnumerable<SelectListItem> SelectListCities(IEnumerable<City> cities);
    }
}

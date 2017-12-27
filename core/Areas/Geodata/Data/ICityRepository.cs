using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tochka.Areas.Geodata.Data
{
    public interface ICityRepository
    {
        IQueryable<City> Cities { get; }

        IEnumerable<SelectListItem> SelectListCities(IEnumerable<City> cities);

        Task<IEnumerable<City>> RepresentationCitiesAsync();
    }
}

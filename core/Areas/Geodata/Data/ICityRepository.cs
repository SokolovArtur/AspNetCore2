using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tochka.Areas.Geodata.Data
{
    public interface ICityRepository
    {
        IEnumerable<City> Cities { get; }

        IEnumerable<SelectListItem> CitiesInSelectList(IEnumerable<City> cities);

        Task<IEnumerable<City>> RepresentationCities();
    }
}

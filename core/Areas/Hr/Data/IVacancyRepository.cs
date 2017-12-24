using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tochka.Areas.Hr.Data
{
    public interface IVacancyRepository
    {
        IEnumerable<Vacancy> Vacancies { get; }

        IEnumerable<SelectListItem> CitiesList { get; }

        bool HasDuplicate(Vacancy vacancy);
    }
}

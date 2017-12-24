using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tochka.Areas.Hr.Data
{
    public interface IVacancyRepository
    {
        IEnumerable<Vacancy> Vacancies { get; }

        Task<bool> HasDuplicate(Vacancy vacancy);
    }
}

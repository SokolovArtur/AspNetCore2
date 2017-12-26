using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tochka.Areas.Hr.Data
{
    public interface IVacancyRepository
    {
        IEnumerable<Vacancy> Vacancies { get; }

        Task<IEnumerable<Vacancy>> Duplicates(Vacancy vacancy, int cityId);

        Task<IEnumerable<Vacancy>> Duplicates(Vacancy vacancy, List<int> citiesIds);

        Task SaveAsync(Vacancy vacancy, List<int> citiesIds);
    }
}

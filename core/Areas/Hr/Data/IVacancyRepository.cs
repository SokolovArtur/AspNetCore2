using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tochka.Areas.Hr.Data
{
    public interface IVacancyRepository
    {
        IQueryable<Vacancy> Vacancies { get; }

        Task<IEnumerable<int>> CitiesIdsInVacancyAsync(int vacancyId);

        Task DeleteAsync(Vacancy vacancy);

        Task<IEnumerable<Vacancy>> DuplicatesAsync(Vacancy vacancy, int cityId);

        Task<IEnumerable<Vacancy>> DuplicatesAsync(Vacancy vacancy, List<int> citiesIds);

        Task<Vacancy> FindByIdAsync(int vacancyId);

        Task<IEnumerable<string>> NamesOfCitiesInVacancyAsync(int vacancyId);

        Task SaveAsync(Vacancy vacancy, List<int> citiesIds);
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tochka.Areas.Geodata.Data;
using Tochka.Areas.Hr.Data;

namespace Tochka.Areas.Hr.Services
{
    public interface IVacancyRepository
    {
        IQueryable<Vacancy> Vacancies { get; }

        Task DeleteAsync(Vacancy model);

        Task<Vacancy> FindByIdAsync(int vacancyId);

        IQueryable<City> GetCitiesInVacancy(int vacancyId);
        
        IQueryable<Vacancy> GetDuplicates(Vacancy model, IEnumerable<int> citiesIds);

        Task SaveAsync(Vacancy vacancy, IEnumerable<int> citiesIds);
    }
}
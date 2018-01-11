using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tochka.Areas.Geodata.Data;
using Tochka.Areas.Geodata.Services;
using Tochka.Areas.Hr.Data;
using Tochka.Data;

namespace Tochka.Areas.Hr.Services
{
    public class EFVacancyRepository : IVacancyRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICityRepository _city;

        public EFVacancyRepository(ApplicationDbContext context, ICityRepository city)
        {
            _context = context;
            _city = city;
        }

        public IQueryable<Vacancy> Vacancies => _context.Vacancies.AsNoTracking();

        private IQueryable<VacancyCity> VacanciesCities => _context.VacanciesCities.AsNoTracking();

        public async Task DeleteAsync(Vacancy model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await DeleteAllVacancyCityByVacancyId(model.Id);
                    _context.Remove(model);
                    await _context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (DbUpdateException)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<Vacancy> FindByIdAsync(int vacancyId)
        {
            var vacancy = await Vacancies.SingleOrDefaultAsync(m => m.Id == vacancyId);
            return vacancy;
        }

        public IQueryable<City> GetCitiesInVacancy(int vacancyId)
        {
            return _city.Cities
                .Join(VacanciesCities, city => city.Id, vacancyCity => vacancyCity.CityId,
                    (city, vacancyCity) => new {vacancyCity.VacancyId, city})
                .Where(model => model.VacancyId == vacancyId)
                .Select(model => model.city);
        }

        public IQueryable<Vacancy> GetDuplicates(Vacancy model, IEnumerable<int> citiesIds)
        {
            if (model.Name == null)
            {
                return null;
            }

            citiesIds = citiesIds ?? Enumerable.Empty<int>();
            return VacanciesCities
                .Where(vacancyCity => vacancyCity.Vacancy.Name == model.Name && vacancyCity.Vacancy.Id != model.Id &&
                                      citiesIds.Contains(vacancyCity.CityId))
                .Select(vacancyCity => vacancyCity.Vacancy);
        }

        public async Task SaveAsync(Vacancy model, IEnumerable<int> citiesIds)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                int savedVacancies;
                try
                {
                    if (model.Id > 0)
                    {
                        _context.Update(model);
                    }
                    else
                    {
                        _context.Add(model);
                    }

                    savedVacancies = await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    transaction.Rollback();
                    throw;
                }

                if (savedVacancies > 0)
                {
                    try
                    {
                        var vacanciesCities = new List<VacancyCity>();
                        foreach (var cityId in citiesIds)
                        {
                            vacanciesCities.Add(new VacancyCity
                            {
                                VacancyId = model.Id,
                                CityId = cityId
                            });
                        }
                        await SaveVacancyCityRangeAsync(vacanciesCities);
                    }
                    catch (DbUpdateException)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                transaction.Commit();
            }
        }
        
        private async Task DeleteAllVacancyCityByVacancyId(int vacancyId)
        {
            var vacancyCities = VacanciesCities.Where(vacancyCity => vacancyCity.VacancyId == vacancyId);
            if (vacancyCities.Any())
            {
                _context.RemoveRange(vacancyCities);
                await _context.SaveChangesAsync();
            }
        }

        private async Task DeleteVacancyCityRangeAsync(IEnumerable<VacancyCity> models)
        {
            IEnumerable<int> vacanciesIds = models.GroupBy(vacancyCity => vacancyCity.VacancyId)
                .Select(vacancyCity => vacancyCity.Key);
            foreach (var vacancyId in vacanciesIds)
            {
                await DeleteAllVacancyCityByVacancyId(vacancyId);
            }
        }

        private async Task SaveVacancyCityRangeAsync(IEnumerable<VacancyCity> models)
        {
            if (models.Any())
            {
                await DeleteVacancyCityRangeAsync(models);
                await _context.AddRangeAsync(models);
                await _context.SaveChangesAsync();
            }
        }
    }
}

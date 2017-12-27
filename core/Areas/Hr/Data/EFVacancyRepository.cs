using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Tochka.Data;

namespace Tochka.Areas.Hr.Data
{
    public class EFVacancyRepository : IVacancyRepository
    {
        private readonly ApplicationDbContext _context;

        public EFVacancyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Vacancy> Vacancies => _context.Vacancies.AsNoTracking();

        private IQueryable<VacancyCity> VacanciesCities => _context.VacanciesCities.AsNoTracking();

        public async Task<IEnumerable<int>> CitiesIdsInVacancyAsync(int vacancyId)
        {
            return await VacanciesCities.Where(vc => vc.VacancyId == vacancyId).Select(vc => vc.CityId).ToListAsync();
        }

        public async Task DeleteAsync(int vacancyId)
        {
            var vacancy = await FindByIdAsync(vacancyId);
            if (vacancy != null)
            {
                await DeleteAsync(vacancy);
            }
        }

        public async Task DeleteAsync(Vacancy vacancy)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await DeleteVacancyCityRangeAsync(new List<Vacancy> { vacancy });
                    _context.Remove(vacancy);
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

        public async Task<IEnumerable<Vacancy>> DuplicatesAsync(Vacancy vacancy, int cityId)
        {
            return await DuplicatesAsync(vacancy, new List<int> { cityId });
        }

        public async Task<IEnumerable<Vacancy>> DuplicatesAsync(Vacancy vacancy, List<int> citiesIds)
        {
            IEnumerable<Vacancy> duplicates = await VacanciesCities
                .Where(vc =>
                    citiesIds.Contains(vc.CityId)
                    && vc.Vacancy.Name == vacancy.Name
                    && vc.Vacancy.Id != vacancy.Id)
                .Select(vc => new Vacancy
                {
                    Id = vc.Vacancy.Id,
                    Name = vc.Vacancy.Name,
                    LatinName = vc.Vacancy.LatinName,
                    Annotation = vc.Vacancy.Annotation,
                    Text = vc.Vacancy.Text
                })
                .ToListAsync();
            return duplicates;
        }

        public async Task<Vacancy> FindByIdAsync(int vacancyId)
        {
            var vacancy = await Vacancies.SingleOrDefaultAsync(m => m.Id == vacancyId);
            return vacancy;
        }

        public async Task SaveAsync(Vacancy vacancy, List<int> citiesIds)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                int savedVacancies = 0;
                try
                {
                    if (vacancy.Id > 0)
                    {
                        _context.Update(vacancy);
                    }
                    else
                    {
                        _context.Add(vacancy);
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
                        foreach (int cityId in citiesIds)
                        {
                            vacanciesCities.Add(new VacancyCity
                            {
                                VacancyId = vacancy.Id,
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

        private async Task DeleteVacancyCityRangeAsync(IEnumerable<VacancyCity> vacanciesCities)
        {
            IEnumerable<int> vacanciesIds = vacanciesCities.GroupBy(vc => vc.VacancyId).Select(vc => vc.Key);
            await DeleteVacancyCityRangeAsync(vacanciesIds);
        }

        private async Task DeleteVacancyCityRangeAsync(IEnumerable<Vacancy> vacancies)
        {
            List<int> vacanciesIds = new List<int>();
            vacanciesIds.AddRange(vacancies.Select(v => v.Id));
            await DeleteVacancyCityRangeAsync(vacanciesIds);
        }

        private async Task DeleteVacancyCityRangeAsync(IEnumerable<int> vacanciesIds)
        {
            foreach (var vacancyId in vacanciesIds)
            {
                _context.RemoveRange(VacanciesCities.Where(vm => vm.VacancyId == vacancyId));
            }
            await _context.SaveChangesAsync();
        }

        private async Task SaveVacancyCityRangeAsync(IEnumerable<VacancyCity> vacanciesCities)
        {
            try
            {
                await DeleteVacancyCityRangeAsync(vacanciesCities);
            }
            catch (DbUpdateException)
            {
                throw;
            }

            try
            {
                await _context.AddRangeAsync(
                    vacanciesCities.Select(vc => new VacancyCity
                    {
                        VacancyId = vc.VacancyId,
                        CityId = vc.CityId
                    })
                );
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }
    }
}

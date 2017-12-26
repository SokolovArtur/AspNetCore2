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

        public IEnumerable<Vacancy> Vacancies => _context.Vacancies;

        public async Task<IEnumerable<Vacancy>> Duplicates(Vacancy vacancy, int cityId)
        {
            return await Duplicates(vacancy, new List<int> { cityId });
        }

        public async Task<IEnumerable<Vacancy>> Duplicates(Vacancy vacancy, List<int> citiesIds)
        {
            IEnumerable<Vacancy> duplicates = await _context.VacanciesCities
                .Where(vc =>
                    citiesIds.Contains(vc.CityId)
                    && vc.Vacancy.Name == vacancy.Name
                    && vc.Vacancy.Id != vacancy.Id)
                .Select(vc => new Vacancy
                {
                    Id = vc.Vacancy.Id,
                    Name = vc.Vacancy.Name,
                    Ref = vc.Vacancy.Ref,
                    Annotation = vc.Vacancy.Annotation,
                    Text = vc.Vacancy.Text
                })
                .ToListAsync();
            return duplicates;
        }

        public async Task SaveAsync(Vacancy vacancy, List<int> citiesIds)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                int savedVacancies = 0;
                try
                {
                    EntityEntry ee = (vacancy.Id > 0) ? _context.Update(vacancy) : _context.Add(vacancy);
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
            foreach (var vacancyCity in vacanciesCities)
            {
                _context.RemoveRange(_context.VacanciesCities.Where(
                    vm => (vm.VacancyId == vacancyCity.VacancyId && vm.CityId == vacancyCity.CityId)
                ));
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

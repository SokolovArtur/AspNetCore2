using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> HasDuplicate(Vacancy vacancy)
        {
            var vacancies = await _context.Vacancies
                .SingleOrDefaultAsync(m => m.Ref == vacancy.Ref && m.Id != vacancy.Id);
            return (vacancies == null) ? false : true;
        }
    }
}

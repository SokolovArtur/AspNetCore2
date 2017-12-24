using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IEnumerable<SelectListItem> CitiesList
        {
            get
            {
                return
                    from model in _context.Cities
                    where model.IsRepresentation == true
                    select new SelectListItem { Value = model.Id.ToString(), Text = model.Name };
            }
        }

        public bool HasDuplicate(Vacancy vacancy)
        {
            IEnumerable<Vacancy> vacanciesOfSameName =
                from model in _context.Vacancies
                where model.Name == vacancy.Name && model.Id != vacancy.Id
                select model;
            foreach (Vacancy vacancyOfSameName in vacanciesOfSameName)
            {
                IEnumerable<VacancyCity> duplicatesOfVacancy =
                    from model in vacancyOfSameName.VacanciesCities
                    where model.City.Name == vacancy.Name
                    select model;
                if (duplicatesOfVacancy != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

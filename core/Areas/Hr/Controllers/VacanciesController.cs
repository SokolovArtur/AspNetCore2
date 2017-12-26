using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tochka.Areas.Geodata.Data;
using Tochka.Areas.Hr.Data;
using Tochka.Areas.Hr.Models;
using Tochka.Data;

namespace Tochka.Areas.Hr.Controllers
{
    [Area("Hr")]
    public class VacanciesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICityRepository _city;
        private readonly IVacancyRepository _vacancy;

        public VacanciesController(
            ApplicationDbContext context,
            ICityRepository city,
            IVacancyRepository vacancy)
        {
            _context = context;
            _city = city;
            _vacancy = vacancy;
        }

        // GET: Hr/Vacancies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vacancies.ToListAsync());
        }

        // GET: Hr/Vacancies/Details/0
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancies
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vacancy == null)
            {
                return NotFound();
            }

            return View(vacancy);
        }

        // GET: Hr/Vacancies/Create
        public async Task<IActionResult> Create()
        {
            return View(new VacancyRecordViewModel
            {
                CitiesForSelection = _city.CitiesInSelectList(await _city.RepresentationCities())
            });
        }

        // POST: Hr/Vacancies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ListCitiesIds,Annotation,Text")] VacancyRecordViewModel vm)
        {
            vm.CitiesForSelection = _city.CitiesInSelectList(await _city.RepresentationCities());

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Vacancy vacancy = new Vacancy
            {
                Name = vm.Name,
                Ref = vm.Ref,
                Annotation = vm.Annotation,
                Text = vm.Text,
            };
            var citiesIds = new List<int>();
            citiesIds.AddRange(vm.ListCitiesIds);

            IEnumerable<Vacancy> duplicates = await _vacancy.Duplicates(vacancy, citiesIds);
            if (duplicates.Count() > 0)
            {
                ModelState.AddModelError("", "Duplicate value.");
                return View(vm);
            }

            try
            {
                await _vacancy.SaveAsync(vacancy, citiesIds);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Error saving.");
                return View(vm);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Hr/Vacancies/Edit/0
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancies.SingleOrDefaultAsync(m => m.Id == id);
            if (vacancy == null)
            {
                return NotFound();
            }

            return View(new VacancyRecordViewModel
            {
                Id = vacancy.Id,
                Name = vacancy.Name,
                Annotation = vacancy.Annotation,
                Text = vacancy.Text
            });
        }

        // POST: Hr/Vacancies/Edit/0
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ListCitiesIds,Annotation,Text")] VacancyRecordViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            Vacancy vacancy = new Vacancy
            {
                Id = vm.Id,
                Name = vm.Name,
                Ref = vm.Ref,
                Annotation = vm.Annotation,
                Text = vm.Text
            };

            List<int> citiesIds = new List<int>();
            citiesIds.AddRange(vm.ListCitiesIds);

            IEnumerable<Vacancy> duplicates = await _vacancy.Duplicates(vacancy, citiesIds);
            if (duplicates.Count() > 0)
            {
                ModelState.AddModelError("", "Duplicate value.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacancy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacancyExists(vacancy.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: Hr/Vacancies/Delete/0
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacancy = await _context.Vacancies
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vacancy == null)
            {
                return NotFound();
            }

            return View(vacancy);
        }

        // POST: Hr/Vacancies/Delete/0
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacancy = await _context.Vacancies.SingleOrDefaultAsync(m => m.Id == id);
            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacancyExists(int id)
        {
            return _context.Vacancies.Any(e => e.Id == id);
        }
    }
}

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tochka.Areas.Hr.Data;
using Tochka.Areas.Hr.Models;
using Tochka.Data;

namespace Tochka.Areas.Hr.Controllers
{
    [Area("Hr")]
    public class VacanciesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IVacancyRepository _repository;

        public VacanciesController(ApplicationDbContext context, IVacancyRepository repository)
        {
            _context = context;
            _repository = repository;
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
        public IActionResult Create()
        {
            return View(new VacancyRecordViewModel(_repository.CitiesList));
        }

        // POST: Hr/Vacancies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,City,Annotation,Text")] VacancyRecordViewModel vm)
        {
            Vacancy vacancy = new Vacancy(
                vm.Name,
                vm.Ref,
                vm.Annotation,
                vm.Text
            );

            if (_repository.HasDuplicate(vacancy))
            {
                ModelState.AddModelError("Name", "Duplicate value");
            }

            if (ModelState.IsValid)
            {
                _context.Add(vacancy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
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

            return View(new VacancyRecordViewModel(
                vacancy.Id,
                vacancy.Name,
                _repository.CitiesList,
                1, // TODO
                vacancy.Annotation,
                vacancy.Text
            ));
        }

        // POST: Hr/Vacancies/Edit/0
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,City,Annotation,Text")] VacancyRecordViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            Vacancy vacancy = new Vacancy(
                vm.Id,
                vm.Name,
                vm.Ref,
                vm.Annotation,
                vm.Text
            );

            if (_repository.HasDuplicate(vacancy))
            {
                ModelState.AddModelError("Name", "Duplicate value");
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

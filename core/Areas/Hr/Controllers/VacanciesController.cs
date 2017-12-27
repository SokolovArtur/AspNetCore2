using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tochka.Areas.Geodata.Data;
using Tochka.Areas.Hr.Data;
using Tochka.Areas.Hr.Models;

namespace Tochka.Areas.Hr.Controllers
{
    [Area("Hr")]
    public class VacanciesController : Controller
    {
        private readonly ICityRepository _city;
        private readonly IVacancyRepository _vacancy;

        public VacanciesController(ICityRepository city, IVacancyRepository vacancy)
        {
            _city = city;
            _vacancy = vacancy;
        }

        // GET: Hr/Vacancies
        public async Task<IActionResult> Index()
        {
            IEnumerable<Vacancy> vacancies = await _vacancy.Vacancies.ToListAsync();
            var vm = new List<VacancyItemViewModel>();
            foreach (Vacancy vacancy in vacancies)
            {
                vm.Add(new VacancyItemViewModel
                {
                    Id = vacancy.Id,
                    Name = vacancy.Name,
                    Annotation = vacancy.Annotation,
                    Text = vacancy.Text,
                    NamesOfCities = await _vacancy.NamesOfCitiesInVacancyAsync(vacancy.Id)
                });
            }
            return View(vm);
        }

        // GET: Hr/Vacancies/Details/0
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacancy = await _vacancy.FindByIdAsync((int)id);
            if (vacancy == null)
            {
                return NotFound();
            }

            return View(new VacancyItemViewModel
            {
                Id = vacancy.Id,
                Name = vacancy.Name,
                Annotation = vacancy.Annotation,
                Text = vacancy.Text,
                NamesOfCities = await _vacancy.NamesOfCitiesInVacancyAsync(vacancy.Id)
            });
        }

        // GET: Hr/Vacancies/Create
        public async Task<IActionResult> Create()
        {
            return View(new VacancyRecordViewModel
            {
                CitiesForSelection = _city.SelectListCities(await _city.RepresentationCitiesAsync())
            });
        }

        // POST: Hr/Vacancies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ListCitiesIds,Annotation,Text")] VacancyRecordViewModel vm)
        {
            vm.CitiesForSelection = _city.SelectListCities(await _city.RepresentationCitiesAsync());

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Vacancy vacancy = new Vacancy
            {
                Name = vm.Name,
                LatinName = vm.LatinName,
                Annotation = vm.Annotation,
                Text = vm.Text,
            };
            var citiesIds = new List<int>();
            citiesIds.AddRange(vm.ListCitiesIds);

            IEnumerable<Vacancy> duplicates = await _vacancy.DuplicatesAsync(vacancy, citiesIds);
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

            var vacancy = await _vacancy.FindByIdAsync((int)id);
            if (vacancy == null)
            {
                return NotFound();
            }

            return View(new VacancyRecordViewModel
            {
                Id = vacancy.Id,
                Name = vacancy.Name,
                Annotation = vacancy.Annotation,
                Text = vacancy.Text,
                CitiesForSelection = _city.SelectListCities(await _city.RepresentationCitiesAsync()),
                ListCitiesIds = await _vacancy.CitiesIdsInVacancyAsync(vacancy.Id)
            });
        }

        // POST: Hr/Vacancies/Edit/0
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ListCitiesIds,Annotation,Text")] VacancyRecordViewModel vm)
        {
            if (id != vm.Id || (await _vacancy.FindByIdAsync((int)id) == null))
            {
                return NotFound();
            }

            vm.CitiesForSelection = _city.SelectListCities(await _city.RepresentationCitiesAsync());

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Vacancy vacancy = new Vacancy
            {
                Id = vm.Id,
                Name = vm.Name,
                LatinName = vm.LatinName,
                Annotation = vm.Annotation,
                Text = vm.Text,
            };
            var citiesIds = new List<int>();
            citiesIds.AddRange(vm.ListCitiesIds);

            IEnumerable<Vacancy> duplicates = await _vacancy.DuplicatesAsync(vacancy, citiesIds);
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

        // GET: Hr/Vacancies/Delete/0
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vacancy vacancy = await _vacancy.FindByIdAsync((int)id);
            if (vacancy == null)
            {
                return NotFound();
            }

            await _vacancy.DeleteAsync(vacancy);
            return RedirectToAction(nameof(Index));
        }
    }
}

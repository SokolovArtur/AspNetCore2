using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tochka.Areas.Geodata.Data;
using Tochka.Areas.Hr.Data;
using Tochka.Areas.Hr.Models.VacancyViewModels;

namespace Tochka.Areas.Hr.Controllers
{
    [Area("Hr")]
    public class VacancyController : Controller
    {
        private readonly ICityRepository _city;
        private readonly IVacancyRepository _vacancy;

        public VacancyController(ICityRepository city, IVacancyRepository vacancy)
        {
            _city = city;
            _vacancy = vacancy;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Vacancy> vacancies = await _vacancy.Vacancies.ToListAsync();
            var model = new List<ItemViewModel>();
            foreach (Vacancy vacancy in vacancies)
            {
                model.Add(new ItemViewModel
                {
                    Id = vacancy.Id,
                    Name = vacancy.Name,
                    Annotation = vacancy.Annotation,
                    Text = vacancy.Text,
                    NamesOfCities = await _vacancy.NamesOfCitiesInVacancyAsync(vacancy.Id)
                });
            }
            return View(model);
        }

        [HttpGet]
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

            return View(new ItemViewModel
            {
                Id = vacancy.Id,
                Name = vacancy.Name,
                Annotation = vacancy.Annotation,
                Text = vacancy.Text,
                NamesOfCities = await _vacancy.NamesOfCitiesInVacancyAsync(vacancy.Id)
            });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new RecordViewModel
            {
                CitiesForSelection = _city.SelectListCities(await _city.RepresentationCitiesAsync())
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ListCitiesIds,Annotation,Text")] RecordViewModel model)
        {
            model.CitiesForSelection = _city.SelectListCities(await _city.RepresentationCitiesAsync());

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Vacancy vacancy = new Vacancy
            {
                Name = model.Name,
                LatinName = model.LatinName,
                Annotation = model.Annotation,
                Text = model.Text,
            };
            var citiesIds = new List<int>();
            citiesIds.AddRange(model.ListCitiesIds);

            IEnumerable<Vacancy> duplicates = await _vacancy.DuplicatesAsync(vacancy, citiesIds);
            if (duplicates.Count() > 0)
            {
                ModelState.AddModelError(string.Empty, "Duplicate");
                return View(model);
            }

            try
            {
                await _vacancy.SaveAsync(vacancy, citiesIds);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "Error of saving data");
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
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

            return View(new RecordViewModel
            {
                Id = vacancy.Id,
                Name = vacancy.Name,
                Annotation = vacancy.Annotation,
                Text = vacancy.Text,
                CitiesForSelection = _city.SelectListCities(await _city.RepresentationCitiesAsync()),
                ListCitiesIds = await _vacancy.CitiesIdsInVacancyAsync(vacancy.Id)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ListCitiesIds,Annotation,Text")] RecordViewModel model)
        {
            if (id != model.Id || (await _vacancy.FindByIdAsync((int)id) == null))
            {
                return NotFound();
            }

            model.CitiesForSelection = _city.SelectListCities(await _city.RepresentationCitiesAsync());

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Vacancy vacancy = new Vacancy
            {
                Id = model.Id,
                Name = model.Name,
                LatinName = model.LatinName,
                Annotation = model.Annotation,
                Text = model.Text,
            };
            var citiesIds = new List<int>();
            citiesIds.AddRange(model.ListCitiesIds);

            IEnumerable<Vacancy> duplicates = await _vacancy.DuplicatesAsync(vacancy, citiesIds);
            if (duplicates.Count() > 0)
            {
                ModelState.AddModelError(string.Empty, "Duplicate");
                return View(model);
            }

            try
            {
                await _vacancy.SaveAsync(vacancy, citiesIds);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "Error of saving data");
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
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

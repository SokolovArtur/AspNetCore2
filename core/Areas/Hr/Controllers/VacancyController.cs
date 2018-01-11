using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tochka.Areas.Geodata.Services;
using Tochka.Areas.Hr.Data;
using Tochka.Areas.Hr.Models.VacancyViewModels;
using Tochka.Areas.Hr.Services;

namespace Tochka.Areas.Hr.Controllers
{
    [Area("Hr")]
    [Authorize]
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
            var model = vacancies.Select(vacancy => new ItemViewModel
            {
                Id = vacancy.Id,
                Name = vacancy.Name,
                Annotation = vacancy.Annotation,
                Text = vacancy.Text,
                NamesOfCities = _vacancy.GetCitiesInVacancy(vacancy.Id).Select(city => city.Name)
            });
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
                NamesOfCities = await _vacancy.GetCitiesInVacancy(vacancy.Id).Select(city => city.Name).ToListAsync()
            });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new RecordViewModel
            {
                CitiesForSelection = _city.SelectListCities(await _city.GetRepresentationCities().ToListAsync())
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ListCitiesIds,Annotation,Text")] RecordViewModel model)
        {
            model.CitiesForSelection = _city.SelectListCities(await _city.GetRepresentationCities().ToListAsync());

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
                CitiesForSelection = _city.SelectListCities(await _city.GetRepresentationCities().ToListAsync()),
                ListCitiesIds = await _vacancy.GetCitiesInVacancy(vacancy.Id).Select(city => city.Id).ToListAsync()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ListCitiesIds,Annotation,Text")] RecordViewModel model)
        {
            if (id != model.Id || await _vacancy.FindByIdAsync(id) == null)
            {
                return BadRequest();
            }

            model.CitiesForSelection = _city.SelectListCities(await _city.GetRepresentationCities().ToListAsync());

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

        [HttpPost]
        [ValidateAntiForgeryToken]
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
            
            try
            {
                await _vacancy.DeleteAsync(vacancy);
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { Code = "NotDeleted", Description = "Error deleting data" });
            }
            
            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet]
        public IActionResult RemoteVacancyIsUnique(IEnumerable<int> listCitiesIds, int id, string name)
        {
            var result = _vacancy.GetDuplicates(new Vacancy { Id = id, Name = name }, listCitiesIds);
            return Json(result == null || !result.Any());
        }
    }
}

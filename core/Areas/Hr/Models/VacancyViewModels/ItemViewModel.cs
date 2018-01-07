using System.Collections.Generic;

namespace Tochka.Areas.Hr.Models.VacancyViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Annotation { get; set; }

        public string Text { get; set; }

        public IEnumerable<string> NamesOfCities { get; set; }

        public ItemViewModel() { }
    }
}

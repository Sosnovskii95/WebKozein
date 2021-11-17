using WebKozein.Models.CodeFirst;

namespace WebKozein.Models.FilterSortView
{
    public class IndexViewModel
    {
        public IEnumerable<InformDataBase> InformDataBases { get; set; }

        public FilterViewModel FilterViewModel { get; set; }

        public SortViewModel SortViewModel { get; set; }
    }
}

using WebKozein.Models.CodeFirst;
using WebKozein.Models.FilterSortView;

namespace WebKozein.Models.FilterSortView
{
    public class IndexViewModel
    {
        public IEnumerable<InformDataBase> InformDataBases { get; set; }

        public FilterViewModel FilterViewModel { get; set; }

        public SortViewModel SortViewModel { get; set; }
    }
}

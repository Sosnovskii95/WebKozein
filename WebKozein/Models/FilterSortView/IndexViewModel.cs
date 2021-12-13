using WebKozein.Models.CodeFirst;
using WebKozein.Models.ComboBox;
using WebKozein.Models.Weight;

namespace WebKozein.Models.FilterSortView
{
    public class IndexViewModel
    {
        public IEnumerable<InformDataBase> InformDataBases { get; set; }

        public FilterViewModel FilterViewModel { get; set; }

        public SortViewModel SortViewModel { get; set; }
    }
}

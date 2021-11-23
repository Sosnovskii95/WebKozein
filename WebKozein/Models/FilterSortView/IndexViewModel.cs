using WebKozein.Models.CodeFirst;
using WebKozein.Models.ComboBox;

namespace WebKozein.Models.FilterSortView
{
    public class IndexViewModel
    {
        public IEnumerable<InformDataBase> InformDataBases { get; set; }

        public IEnumerable<TableComboBox> TableComboBoxes { get; set; }

        public FilterViewModel FilterViewModel { get; set; }

        public SortViewModel SortViewModel { get; set; }
    }
}

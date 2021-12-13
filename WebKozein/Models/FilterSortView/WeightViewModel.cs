using WebKozein.Models.Weight;
using WebKozein.Models.ComboBox;
namespace WebKozein.Models.FilterSortView
{
    public class WeightViewModel
    {
        public WeightModel WeightModel { get; set; }

        public IEnumerable<TableComboBox> TableComboBoxes { get; set; }
    }
}

namespace WebKozein.Models.FilterSortView
{
    public class FilterViewModel
    {
        public int? SelectedCost { get; private set; }
        public int? SelectedElectricity { get; private set; }
        public int? SelectedPower { get; private set; }
        public int? SelectedPowerTime { get; private set; }

        public FilterViewModel(int? searchCost, int? searchElectricity, int? searchPower, int? searchPowerTime)
        {
            SelectedCost = searchCost;
            SelectedElectricity = searchElectricity;
            SelectedPower = searchPower;
            SelectedPowerTime = searchPowerTime;
        }
    }
}

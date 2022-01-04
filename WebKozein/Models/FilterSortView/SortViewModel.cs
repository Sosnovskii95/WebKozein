namespace WebKozein.Models.FilterSortView
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState NameSort { get; private set; }
        public SortState CostSort { get; private set; }
        public SortState ElectricitySort { get; private set; }
        public SortState WaterSort { get; private set; }
        public SortState AirSort { get; private set; }
        public SortState PowerSort { get; private set; }
        public SortState PowerTimeSort { get; private set; }
        public SortState WeightSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            CostSort = sortOrder == SortState.CostAsc ? SortState.CostDesc : SortState.CostAsc;
            ElectricitySort = sortOrder == SortState.ElectricityAsc ? SortState.ElectricityDesc : SortState.ElectricityAsc;
            WaterSort = sortOrder == SortState.WaterAsc ? SortState.WaterDesc : SortState.WaterAsc;
            AirSort = sortOrder == SortState.AirAsc ? SortState.AirDesc : SortState.AirAsc;
            PowerSort = sortOrder == SortState.PowerAsc ? SortState.PowerDesc : SortState.PowerAsc;
            PowerTimeSort = sortOrder == SortState.PowerTimeAsc ? SortState.PowerTimeDesc : SortState.PowerTimeAsc;
            WeightSort = sortOrder == SortState.WeightAsc ? SortState.WeightDesc : SortState.WeightAsc;
            Current = sortOrder;
        }
    }
}

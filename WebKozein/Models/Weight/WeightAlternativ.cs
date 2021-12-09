using WebKozein.Models.Alternativ;
using WebKozein.Models.ComboBox;

namespace WebKozein.Models.Weight
{
    public class WeightAlternativ : AlternativMethod
    {
        private List<TableComboBox> tableComboBoxList = new List<TableComboBox>();
        private UtilTableComboBox utilTableComboBox = new UtilTableComboBox();
        private double[] massAlternativ;
        private int indexBestAlternativ;

        public WeightAlternativ(List<TableComboBox> tableComboBoxes)
        {
            tableComboBoxList = tableComboBoxes;
            if (tableComboBoxList.Count > 0)
            {
                GetBestWeight();
            }
        }

        public int GetIndexBestAlternativ()
        {
            return indexBestAlternativ;
        }

        public double[] GetMassAlternativ()
        {
            double[] mainAltemp = new double[massAlternativ.Length];
            for (int i = 0; i < massAlternativ.Length; i++)
            {
                mainAltemp[i] = Math.Round(massAlternativ[i], 2);
            }

            return mainAltemp;
        }

        private void GetBestWeight()
        {
            double[] priceAl1 = GetPriceAlternative(utilTableComboBox.getMatrixFromList(1, tableComboBoxList));
            double[] priceAl2 = GetPriceAlternative(utilTableComboBox.getMatrixFromList(2, tableComboBoxList));

            double sumAl1 = GetSummaPriceAlternative(priceAl1);
            double sumAl2 = GetSummaPriceAlternative(priceAl2);

            double[] weightAl1 = GetWeight(priceAl1, sumAl1);
            double[] weightAl2 = GetWeight(priceAl2, sumAl2);

            massAlternativ = GetMainAlternative(weightAl1, weightAl2);

            indexBestAlternativ = GetIndexBestWeigth(massAlternativ);
        }
    }

}

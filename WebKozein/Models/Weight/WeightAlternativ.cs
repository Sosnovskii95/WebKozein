using WebKozein.Data;
using WebKozein.Models.ComboBox;

namespace WebKozein.Models.Weight
{
    public class WeightAlternativ
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
            else
            {
                massAlternativ = new double[6];
            }
        }

        public int GetIndexBestAlternativ()
        {
            return indexBestAlternativ;
        }

        public double[] GetMassAlternativ()
        {
            double[] mainAltemp = new double[massAlternativ.Length + 1];
            for (int i = 0; i < massAlternativ.Length; i++)
            {
                mainAltemp[i] = Math.Round(massAlternativ[i], 2);
            }
            mainAltemp[massAlternativ.Length] = Math.Round(massAlternativ[indexBestAlternativ], 2);

            return mainAltemp;
        }

        private void GetBestWeight()
        {
            double[] priceAl1 = getPriceAlternative(utilTableComboBox.getMatrixFromList(1, tableComboBoxList));
            double[] priceAl2 = getPriceAlternative(utilTableComboBox.getMatrixFromList(2, tableComboBoxList));

            double sumAl1 = GetSummaPriceAlternative(priceAl1);
            double sumAl2 = GetSummaPriceAlternative(priceAl2);

            double[] weightAl1 = GetWeight(priceAl1, sumAl1);
            double[] weightAl2 = GetWeight(priceAl2, sumAl2);

            massAlternativ = GetMainAlternative(weightAl1, weightAl2);

            indexBestAlternativ = GetIndesBestWeigth(massAlternativ);
        }

        private double[] getPriceAlternative(double[,] dataValue)
        {
            double[] priceAlternative = new double[(int)Math.Sqrt(dataValue.Length)];
            for (int i = 0; i < priceAlternative.Length; i++)
            {
                double price = 1;
                for (int j = 0; j < priceAlternative.Length; j++)
                {
                    price *= dataValue[i, j];
                }
                priceAlternative[i] = (double)Math.Pow((double)price, (double)1 / priceAlternative.Length);
            }
            return priceAlternative;
        }

        private double GetSummaPriceAlternative(double[] priceAlternative)
        {
            double sum = 0;
            foreach (double value in priceAlternative)
                sum += value;
            return sum;
        }

        private double[] GetWeight(double[] priceAlternative, double summaAlternative)
        {
            double[] weights = new double[priceAlternative.Length];
            for (int i = 0; i < priceAlternative.Length; i++)
            {
                weights[i] = priceAlternative[i] / summaAlternative;
            }
            return weights;
        }

        private double[] GetMainAlternative(double[] weightAlternative1, double[] weightAlternative2)
        {
            double[] mainAlternative = new double[weightAlternative1.Length];
            for (int i = 0; i < weightAlternative1.Length; i++)
            {
                mainAlternative[i] = (weightAlternative1[i] + weightAlternative2[i]) / 2;
            }
            return mainAlternative;
        }

        private int GetIndesBestWeigth(double[] values)
        {
            double compare = values[0];
            int index = 0;
            for (int i = 1; i < values.Length; i++)
            {
                if (compare < values[i])
                {
                    index = i;
                    compare = values[i];
                }
            }
            return index;
        }
    }

}

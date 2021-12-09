namespace WebKozein.Models.Alternativ
{
    public class AlternativMethod
    {
        public double[] GetPriceAlternative(double[,] dataValue)
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
        public double GetSummaPriceAlternative(double[] priceAlternative)
        {
            double sum = 0;
            foreach (double value in priceAlternative)
                sum += value;
            return sum;
        }

        public double[] GetWeight(double[] priceAlternative, double summaAlternative)
        {
            double[] weights = new double[priceAlternative.Length];
            for (int i = 0; i < priceAlternative.Length; i++)
            {
                weights[i] = priceAlternative[i] / summaAlternative;
            }
            return weights;
        }

        public double[] GetMainAlternative(double[] weightAlternative1, double[] weightAlternative2)
        {
            double[] mainAlternative = new double[weightAlternative1.Length];
            for (int i = 0; i < weightAlternative1.Length; i++)
            {
                mainAlternative[i] = (weightAlternative1[i] + weightAlternative2[i]) / 2;
            }
            return mainAlternative;
        }

        public int GetIndexBestWeigth(double[] values)
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

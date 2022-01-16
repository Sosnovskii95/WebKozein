using WebKozein.Models.CodeFirst;
using WebKozein.Models.Alternativ;


namespace WebKozein.Models.Hierarchy
{
    public class HierarchyMethod : AlternativMethod
    {
        private List<InformDataBase> _informsList;
        private double[] _mainMassAlternativ;
        private List<double[]> listMasParametres = new List<double[]>();

        private double[] weightAlternativs;
        private int indexBestAlternativ;

        public int GetIndexBestAlternativ()
        {
            return indexBestAlternativ;
        }

        public List<InformDataBase> GetInformDataBases()
        {
            return _informsList;
        }

        public HierarchyMethod(List<InformDataBase> informDataBases, double[] mainMassAlternativ)
        {
            _informsList = informDataBases;
            _mainMassAlternativ = mainMassAlternativ;
            if (_informsList.Count > 0)
            {
                GetBestAlternativ();
            }
        }

        private void GetBestAlternativ()
        {
            if (_informsList.Count > 0)
            {
                ConvertListParametresToListMas();
                bool[] status = new bool[5] { true, true, false, true, true };

                List<double[,]> pricesList = new List<double[,]>();
                for (int i = 0; i < listMasParametres.Count; i++)
                {
                    pricesList.Add(CreateMatrixPrices(listMasParametres[i], status[i]));
                }

                List<double[]> pricesAlternativ = new List<double[]>();
                foreach (var item in pricesList)
                {
                    pricesAlternativ.Add(GetPriceAlternative(item));
                }

                List<double> sumAlternativ = new List<double>();
                foreach (var item in pricesAlternativ)
                {
                    sumAlternativ.Add(GetSummaPriceAlternative(item));
                }

                List<double[]> weightAlternativ = new List<double[]>();
                for (int i = 0; i < sumAlternativ.Count; i++)
                {
                    weightAlternativ.Add(GetWeight(pricesAlternativ[i], sumAlternativ[i]));
                }

                double[,] b = CreateMatrixWeight(weightAlternativ);
                weightAlternativs = Stolb(b, _mainMassAlternativ);
                indexBestAlternativ = GetIndexBestWeigth(weightAlternativs);

                for (int i = 0; i < _informsList.Count; i++)
                {
                    _informsList[i].Weight = Convert.ToInt32(Math.Round(weightAlternativs[i], 2) * 100);
                }
            }
        }

        private void ConvertListParametresToListMas()
        {
            List<List<double>> masListParametres = new List<List<double>>();
            for (int i = 0; i < _informsList.Count; i++)
            {
                masListParametres.Add(new List<double>());
                masListParametres[i].Add(_informsList[i].Cost);
                masListParametres[i].Add(_informsList[i].Electricity);
                masListParametres[i].Add(_informsList[i].Power);
                masListParametres[i].Add(_informsList[i].Water);
                masListParametres[i].Add(Convert.ToDouble(_informsList[i].Air) + 1);
            }

            listMasParametres = new List<double[]>();
            for (int i = 0; i < masListParametres[0].Count; i++)
            {
                listMasParametres.Add(new double[masListParametres.Count]);
                for (int j = 0; j < masListParametres.Count; j++)
                {
                    listMasParametres[i][j] = masListParametres[j][i];
                }

            }
        }

        private double[,] CreateMatrixPrices(double[] values, bool status)
        {
            double[,] matrix = new double[values.Length, values.Length];
            for (int i = 0; i < values.Length - 1; i++)
            {
                for (int j = i + 1; j < values.Length; j++)
                {
                    if (status)
                    {
                        matrix[i, j] = values[j] / values[i];
                        matrix[j, i] = values[i] / values[j];
                    }
                    else
                    {
                        matrix[i, j] = values[i] / values[j];
                        matrix[j, i] = values[j] / values[i];
                    }
                }
            }
            for (int i = 0; i < values.Length; i++)
            {
                matrix[i, i] = 1;
            }
            return matrix;
        }

        private double[] Stolb(double[,] A, double[] B)
        {
            double[] res = new double[A.GetLength(1)];
            for (int row = 0; row < A.GetLength(0); row++)
            {
                for (int col = 0; col < A.GetLength(1); col++)
                {
                    res[col] += A[row, col] * B[row];
                }
            }
            return res;
        }
        private double[,] CreateMatrixWeight(List<double[]> weightsAlternatives)
        {
            double[,] matrix = new double[weightsAlternatives.Count, weightsAlternatives[0].Length];
            for (int i = 0; i < weightsAlternatives.Count; i++)
                for (int j = 0; j < weightsAlternatives[0].Length; j++)
                {
                    matrix[i, j] = weightsAlternatives[i][j];
                }
            return matrix;
        }
    }
}

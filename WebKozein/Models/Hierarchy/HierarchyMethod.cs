using WebKozein.Models.CodeFirst;


namespace WebKozein.Models.Hierarchy
{
    public class HierarchyMethod
    {
        public void genereta()
        {
            List<InformDataBase> informs = new List<InformDataBase>() {
                new InformDataBase { Cost=1, Air=true, Electricity=1, Name="1", Id=1, Power=1, PowerTime=1,Water=1},
            new InformDataBase { Cost=2, Air=false, Electricity=2, Name="2", Id=2, Power=2, PowerTime=2,Water=2}
            };
            List<List<double>> vs = new List<List<double>>(informs.Count);
            for (int i = 0; i < informs.Count; i++)
            {
                vs.Add(new List<double>());
                vs[i].Add(informs[i].Cost);
                vs[i].Add(informs[i].Electricity);
                vs[i].Add(informs[i].Power);
                vs[i].Add(informs[i].Water);
                vs[i].Add(Convert.ToDouble(informs[i].Air));
            }
            outf(vs);
            List<double[]> t = new List<double[]>();
            for (int i = 0; i < vs[0].Count; i++)
            {
                t.Add(new double[vs.Count]);
                for (int j = 0; j < vs.Count; j++)
                {
                    t[i][j] = vs[j][i];
                }

            }
            outf(t);
        }

        private void outf(List<List<double>> vs)
        {
            for (int i = 0; vs.Count > i; i++)
            {
                for (int j = 0; vs[i].Count > j; j++)
                {
                    System.Diagnostics.Debug.Write(vs[i][j] + " ");
                }
                System.Diagnostics.Debug.WriteLine("");
            }
        }

        private void outf(List<double[]> vs)
        {
            for (int i = 0; vs.Count > i; i++)
            {
                for (int j = 0; j < vs[i].Length; j++)
                {
                    System.Diagnostics.Debug.Write(vs[i][j] + " ");
                }
                System.Diagnostics.Debug.WriteLine("");
            }
        }

        private float[,] CreateMatrixPrices(float[] values, bool status)
        {
            float[,] matrix = new float[values.Length, values.Length];
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
    }
}

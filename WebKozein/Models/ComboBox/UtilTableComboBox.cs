namespace WebKozein.Models.ComboBox
{
    public class UtilTableComboBox
    {
        public List<TableComboBox> getDefaultTableComboBox()
        {
            int countList = 10;
            List<TableComboBox> tableComboBoxList = new List<TableComboBox>(countList);

            int count = 0;
            for (int i = 0; i < countList; i++)
            {
                TableComboBox tableComboBox = new TableComboBox()
                {
                    Id = i + 1,
                    BoxAirIdConst = i + 1,
                    BoxCostIdConst = i,
                    BoxElectricityIdConst = i,
                    BoxPowerIdConst = i,
                    BoxWaterIdConst = i
                };

                switch (count)
                {
                    case 0:
                        tableComboBox.BoxCostIdConst = 8;
                        break;
                    case 1:
                        tableComboBox.BoxElectricityIdConst = 8;
                        break;
                    case 2:
                        tableComboBox.BoxPowerIdConst = 8;
                        break;
                    case 3:
                        tableComboBox.BoxWaterIdConst = 8;
                        break;
                    case 4:
                        tableComboBox.BoxAirIdConst = 8;
                        break;
                }
                count++;

                if (count == 5)
                {
                    count = 0;
                }

                tableComboBoxList.Add(tableComboBox);
            }
            return tableComboBoxList;
        }

        public List<TableComboBox> getListByIdList(List<int> Id, List<int> BoxCostIdConst,
            List<int> BoxElectricityIdConst, List<int> BoxPowerIdConst, List<int> BoxWaterIdConst,
            List<int> BoxAirIdConst)
        {
            List<TableComboBox> tableComboBoxes = new List<TableComboBox>();

            for (int i = 0; i < Id.Count; i++)
            {
                tableComboBoxes.Add(new TableComboBox
                {
                    Id = Id[i],
                    BoxAirIdConst = BoxAirIdConst[i],
                    BoxCostIdConst = BoxCostIdConst[i],
                    BoxElectricityIdConst = BoxElectricityIdConst[i],
                    BoxPowerIdConst = BoxPowerIdConst[i],
                    BoxWaterIdConst = BoxWaterIdConst[i]
                });
            }

            return tableComboBoxes;
        }

        public double[,] getMatrixFromList(int itemMatrix, List<TableComboBox> tableComboBoxList)
        {
            TableComboBox[] tableComboBoxes;

            if (itemMatrix == 1)
            {
                tableComboBoxes = tableComboBoxList.Take(5).ToArray();
            }
            else
            {
                tableComboBoxes = tableComboBoxList.Skip(5).ToArray();
            }


            double[,] matrix = new double[5, 5];

            for (int i = 0; i < tableComboBoxes.Length; i++)
            {
                for (int j = 0; j < tableComboBoxes.Length; j++)
                {
                    matrix[i, j] = getValueIdComboBox(tableComboBoxes[i], j);
                }
            }

            return matrix;
        }

        private double getValueIdComboBox(TableComboBox comboBox, int postion)
        {
            UtilConstComboBox utilConstComboBox = new UtilConstComboBox();
            switch (postion)
            {
                case 0: return (utilConstComboBox.getValueId(comboBox.BoxCostIdConst));
                case 1: return (utilConstComboBox.getValueId(comboBox.BoxPowerIdConst));
                case 2: return (utilConstComboBox.getValueId(comboBox.BoxPowerIdConst));
                case 3: return (utilConstComboBox.getValueId(comboBox.BoxWaterIdConst));
                case 4: return (utilConstComboBox.getValueId(comboBox.BoxAirIdConst));
                default: return 0;
            }
        }
    }
}

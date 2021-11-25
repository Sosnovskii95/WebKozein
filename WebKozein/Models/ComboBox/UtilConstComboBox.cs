namespace WebKozein.Models.ComboBox
{
    public class UtilConstComboBox
    {
        private List<ConstComboBox> constComboBoxList;

        public List<ConstComboBox> getConstComboBoxes()
        {
            return constComboBoxList;
        }
        
        public double getValueId(int idConst)
        {
            return constComboBoxList.FirstOrDefault(f => f.IdConst == idConst).ValueConst;
        }

        public UtilConstComboBox()
        {
            constComboBoxList = new List<ConstComboBox>();
            int id = 0;
            for (int i = 1; i < 2; i++)
            {

                for (int j = 9; j > 0; j--)
                {
                    if (i == j)
                    {
                        constComboBoxList.Add(new ConstComboBox
                        {
                            IdConst = id,
                            NameConst = i.ToString(),
                            ValueConst = (double)i / j
                        });
                    }
                    else
                    {
                        constComboBoxList.Add(new ConstComboBox
                        {
                            IdConst = id,
                            NameConst = i + "/" + j,
                            ValueConst = (double)i / j
                        });
                    }
                    id++;
                }
            }

            for (int i = 2; i < 10; i++)
            {
                constComboBoxList.Add(new ConstComboBox
                {
                    IdConst = id,
                    NameConst = i.ToString(),
                    ValueConst = (double)i
                });
                id++;
            }
        }
    }
}

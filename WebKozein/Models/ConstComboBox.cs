namespace WebKozein.Models
{
    public class ConstComboBox
    {
        public int IdConst { get; set; }

        public string NameConst { get; set; }

        public double ValueConst { get; set; }

    }

    public class ConstComboBoxViewModel
    {
        private List<ConstComboBox> ConstComboBoxes;

        public List<ConstComboBox> getConstComboBoxes()
        {
            return ConstComboBoxes;
        }

        public ConstComboBoxViewModel()
        {
            ConstComboBoxes = new List<ConstComboBox>();
            int id = 0;
            for (int i = 1; i < 2; i++)
            {

                for (int j = 9; j > 0; j--)
                {
                    if (i == j)
                    {
                        ConstComboBoxes.Add(new ConstComboBox
                        {
                            IdConst = id,
                            NameConst = i.ToString(),
                            ValueConst = (double)i / j
                        });
                    }
                    else
                    {
                        ConstComboBoxes.Add(new ConstComboBox
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
                ConstComboBoxes.Add(new ConstComboBox
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

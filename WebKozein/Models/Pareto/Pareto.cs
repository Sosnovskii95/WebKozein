using WebKozein.Models.CodeFirst;

namespace WebKozein.Models.Pareto
{
    public class Pareto
    {
        public List<InformDataBase> ParetoSort(List<InformDataBase> informDataBasesPareto)
        {
            return DeleteElementsOnListDown(DeleteElementsOnListUp(informDataBasesPareto));
        }

        private List<InformDataBase> DeleteElementsOnListDown(List<InformDataBase> listInformDataBase)
        {
            for (int i = 0; i < listInformDataBase.Count - 1; i++)
            {
                for (int j = i + 1; j < listInformDataBase.Count; j++)
                {
                    if (listInformDataBase[i].Cost <= listInformDataBase[j].Cost)
                    {
                        if (listInformDataBase[i].Electricity <= listInformDataBase[j].Electricity)
                        {
                            if (listInformDataBase[i].Water <= listInformDataBase[j].Water)
                            {
                                if (listInformDataBase[i].Power >= listInformDataBase[j].Power)
                                {
                                    if (Convert.ToInt32(listInformDataBase[i].Air) <= Convert.ToInt32(listInformDataBase[j].Air))
                                    {
                                        if (listInformDataBase[i].PowerTime <= listInformDataBase[j].PowerTime)
                                        {
                                            listInformDataBase.Remove(listInformDataBase[j]);
                                            if (i != 0)
                                            {
                                                i--;
                                            }
                                            j--;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return listInformDataBase;
        }

        private List<InformDataBase> DeleteElementsOnListUp(List<InformDataBase> listInformDataBase)
        {
            for (int i = 0; i < listInformDataBase.Count - 1; i++)
            {
                for (int j = i + 1; j < listInformDataBase.Count; j++)
                {
                    if (listInformDataBase[i].Cost >= listInformDataBase[j].Cost)
                    {
                        if (listInformDataBase[i].Electricity >= listInformDataBase[j].Electricity)
                        {
                            if (listInformDataBase[i].Water >= listInformDataBase[j].Water)
                            {
                                if (listInformDataBase[i].Power <= listInformDataBase[j].Power)
                                {
                                    if (Convert.ToInt32(listInformDataBase[i].Air) >= Convert.ToInt32(listInformDataBase[j].Air))
                                    {
                                        if (listInformDataBase[i].PowerTime >= listInformDataBase[j].PowerTime)
                                        {
                                            listInformDataBase.Remove(listInformDataBase[j]);
                                            if (i != 0)
                                            {
                                                i--;
                                            }
                                            j--;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return listInformDataBase;
        }
    }
}

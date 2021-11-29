using System.ComponentModel.DataAnnotations;

namespace WebKozein.Models
{
    public class WeightModel
    {
        [Display(Name = "Цена")]
        public double CostWeight { get; set; }

        [Display(Name = "Электроэнергия")]
        public double ElectricityWeight { get; set; }

        [Display(Name = "Мощность")]
        public double PowerWeight { get; set; }

        [Display(Name = "Вода")]
        public double WaterWeight { get; set; }

        [Display(Name = "Воздух")]
        public double AirWeignt { get; set; }

        [Display(Name = "Лучший кретерий")]
        public double BestCriteria { get; set; }

        public string NameBestCriteria { get; set; }
    }
}

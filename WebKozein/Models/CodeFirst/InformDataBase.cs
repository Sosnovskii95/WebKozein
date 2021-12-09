using System.ComponentModel.DataAnnotations;

namespace WebKozein.Models.CodeFirst
{
    public class InformDataBase
    {
        [Key]
        [Display(Name = "Номер")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string? Name { get; set; }

        [Display(Name = "Стоимость (руб.)")]
        public int Cost { get; set; }

        [Display(Name = "Электроэнергия (кВт*ч/т)")]
        public int Electricity { get; set; }

        [Display(Name = "Вода (т)")]
        public int Water { get; set; }

        [Display(Name = "Воздух")]
        public bool Air { get; set; }

        [Display(Name = "Кол-во")]
        public int Power { get; set; }

        [Display(Name = "Время (ч.)")]
        public int PowerTime { get; set; }

        [Display(Name = "Вес в %")]
        public double Weight { get; set; }
    }
}

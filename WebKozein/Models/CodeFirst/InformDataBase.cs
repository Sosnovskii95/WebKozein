using System.ComponentModel.DataAnnotations;

namespace WebKozein.Models.CodeFirst
{
    public class InformDataBase
    {
        [Key]
        [Display(Name = "Номер")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required]
        public string? Name { get; set; }

        [Display(Name = "Стоимость (руб.)")]
        [Required]
        public int Cost { get; set; }

        [Display(Name = "Электроэнергия (кВт*ч/т)")]
        [Required]
        public int Electricity { get; set; }

        [Display(Name = "Вода (т)")]
        [Required]
        public int Water { get; set; }

        [Display(Name = "Воздух")]
        [Required]
        public bool Air { get; set; }

        [Display(Name = "Кол-во")]
        [Required]
        public int Power { get; set; }

        [Display(Name = "Время (ч.)")]
        [Required]
        public int PowerTime { get; set; }

        [Display(Name = "Вес в %")]
        public double Weight { get; set; }
    }
}

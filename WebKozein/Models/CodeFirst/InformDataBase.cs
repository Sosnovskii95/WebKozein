using System.ComponentModel.DataAnnotations;

namespace WebKozein.Models.CodeFirst
{
    public class InformDataBase
    {
        [Key]
        [Display(Name = "Номер")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage ="Название")]
        public string Name { get; set; }

        [Display(Name = "Стоимость (мил. руб)")]
        [Required(ErrorMessage ="Стоимость (руб)")]
        [Range(0, int.MaxValue, ErrorMessage = "Не может быть отрицательным")]
        public int Cost { get; set; }

        [Display(Name = "Электроэнергия (кВт*ч/т)")]
        [Required(ErrorMessage ="Электроэнергия (кВт*ч/т")]
        [Range(0, int.MaxValue, ErrorMessage = "Не может быть отрицательным")]
        public int Electricity { get; set; }

        [Display(Name = "Вода (т)")]
        [Required(ErrorMessage =("Вода (т)"))]
        [Range(0, int.MaxValue, ErrorMessage = "Не может быть отрицательным")]
        public int Water { get; set; }

        [Display(Name = "Воздух")]
        [Required(ErrorMessage ="Воздух")]
        public bool Air { get; set; }

        [Display(Name = "Кол-во")]
        [Required(ErrorMessage ="Кол-во")]
        [Range(0, int.MaxValue, ErrorMessage = "Не может быть отрицательным")]
        public int Power { get; set; }

        [Display(Name = "Время (ч)")]
        [Required(ErrorMessage ="Время (ч)")]
        [Range(0, int.MaxValue, ErrorMessage = "Не может быть отрицательным")]
        public int PowerTime { get; set; }

        [Display(Name = "Вес в %")]
        public int Weight { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebKozein.Models.CodeFirst
{
    public class InformDataBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public int Electricity { get; set; }

        public int Water { get; set; }

        public bool Air { get; set; }

        public int Power { get; set; }

        public int PowerTime { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using WebKozein.Models.CodeFirst;
using WebKozein.Models.ComboBox;

namespace WebKozein.Data
{
    public class InformDbContext : DbContext
    {
        public InformDbContext(DbContextOptions<InformDbContext> optinos) : base(optinos)
        {
            Database.EnsureCreated();
        }

        public DbSet<InformDataBase> InformDataBases { get; set; }

        public DbSet<TableComboBox> TableComboBoxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UtilTableComboBox utilTable = new UtilTableComboBox();
            modelBuilder.Entity<TableComboBox>().HasData(utilTable.getDefaultTableComboBox());
            modelBuilder.Entity<InformDataBase>().HasData(Create(100));
        }

        private List<InformDataBase> Create(int count)
        {
            Random random = new Random();
            List<InformDataBase> dataBases = new List<InformDataBase>(count);

            bool[] air = new bool[2] { true, false };

            for (int i = 0; i < count; i++)
            {
                dataBases.Add(new InformDataBase
                {
                    Id = i + 1,
                    Name = "Линия" + (i + 1).ToString(),
                    Cost = random.Next(1, 100) * random.Next(1, 100),
                    Electricity = random.Next(1, 10000),
                    Water = random.Next(1, 1000),
                    Air = air[random.Next(0, 1)],
                    Power = random.Next(1, 10000),
                    PowerTime = random.Next(1, 1000),
                    Weight = 0
                });
            }

            return dataBases;
        }
    }
}

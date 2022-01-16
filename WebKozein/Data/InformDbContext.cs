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

            for (int i = 0; i < count; i++)
            {
                bool air = true;
                if (i%2 == 0)
                {
                    air = false;
                }
                dataBases.Add(new InformDataBase
                {
                    Id = i + 1,
                    Name = "Линия " + (i + 1).ToString(),
                    Cost = random.Next(100, 600),
                    Electricity = random.Next(50, 250),
                    Water = random.Next(100, 1000),
                    Air = air,
                    Power = random.Next(500, 1000),
                    PowerTime = random.Next(50, 250),
                    Weight = 0
                });
            }

            return dataBases;
        }
    }
}

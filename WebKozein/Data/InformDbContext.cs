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
        }
    }
}

using Microsoft.EntityFrameworkCore;
using WebKozein.Models.CodeFirst;

namespace WebKozein.Data
{
    public class InformDbContext : DbContext
    {
        public InformDbContext(DbContextOptions<InformDbContext> optinos) : base(optinos)
        {
            Database.EnsureCreated();
        }

        public DbSet<InformDataBase> InformDataBases { get; set; }
    }
}

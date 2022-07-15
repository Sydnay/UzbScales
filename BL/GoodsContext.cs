using Microsoft.EntityFrameworkCore;

namespace BL
{
    public interface IGoodsContext
    {
        public DbSet<Good> Goods { get; set; }
    }

    public class GoodsContext : DbContext, IGoodsContext
    {
        public GoodsContext()
        {
        }
        public DbSet<Good> Goods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Filename={Path.Combine("Base", "ScalesLocal.db")}");
                //optionsBuilder.UseMs("Data Source=D:\\\\helloapp.db");
            }
        }
    }
}

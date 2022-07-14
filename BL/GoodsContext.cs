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
            Goods.Load();
        }
        public DbSet<Good> Goods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseMs("Data Source=D:\\\\helloapp.db");
            }
        }
    }
}

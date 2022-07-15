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
                string connection =
                    System.Configuration.ConfigurationManager.
                    ConnectionStrings["ScaleConnection"].ConnectionString;
                optionsBuilder.UseSqlServer(connection);
                
                //optionsBuilder.UseMs("Data Source=D:\\\\helloapp.db");
            }
        }

    }
}

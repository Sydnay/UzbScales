using System.Data.Entity;

namespace AvaloniaApplication2.Models
{

    public class GoodsContext : DbContext
    {
        public GoodsContext() : base("ScaleConnection")
        {
            
        }
        public DbSet<Good> Goods { get; set; }
    }
}

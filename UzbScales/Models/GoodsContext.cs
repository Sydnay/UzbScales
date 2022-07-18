using System.Data.Entity;

namespace UzbScales.Models
{

    public class GoodsContext : DbContext
    {
        public GoodsContext() : base("ScaleConnection")
        {
            Goods.Load();
        }
        public DbSet<Good> Goods { get; set; }
    }
}

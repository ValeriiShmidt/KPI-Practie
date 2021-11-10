using System;
using KpiPractice.Data.Shops;
using KpiPractice.Data.Goods;
using Good = KpiPractice.Data.Goods.Good;
using Microsoft.EntityFrameworkCore;

namespace KpiPractice.Data
{
    public class ShopContext : DbContext
    {
        public DbSet<Shops.Shop> Shops { get; set; }
        public DbSet<Good> Goods { get; set; }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

using System;
using AutoMapper;
using KpiPractice.Data;
using KpiPractice.Data.Shops;
using KpiPractice.Data.Goods;
using Microsoft.EntityFrameworkCore;

namespace KpiPractice.Tests
{
    public class UnitTestHelper
    {
        public static DbContextOptions<ShopContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<ShopContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new ShopContext(options))
            {
                SeedData(context);
            }
            return options;
        }

        public static void SeedData(ShopContext context)
        {
            context.Shops.Add(new Data.Shops.Shop { Id = 1, Count = 12313, Address = "Вулиця Остапа Бендера" });
            context.Goods.Add(new Good {Id = 1, Name = "Уася", Brand = "ксіаомі", Price = 1234, Category = "Shone", Description = "chista tel", Sum = 332412, ShopId = 1});
            context.SaveChanges();
        }

        public static Mapper CreateMapperProfile()
        {
            var myProfile = new ShopDaoProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }
    }
}
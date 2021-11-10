using System;
namespace KpiPractice.Core.Goods
{
    public class Goods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public float Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Sum { get; set; }
        public int ShopId { get; set; }
    }
}

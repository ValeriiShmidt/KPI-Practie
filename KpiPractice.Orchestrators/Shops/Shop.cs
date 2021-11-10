using System.ComponentModel.DataAnnotations;

namespace KpiPractice.Orchestrators.Shops
{
    public class Shop
    {
        [Required]
        public int Id { get; set; }
        [MinLength(1)]
        [MaxLength(255)]
        public string Address { get; set; }
        [Range(1,500)]
        public int Count { get; set; }
    }
}
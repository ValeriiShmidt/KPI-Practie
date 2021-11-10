using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KpiPractice.Data.Goods
{
    [Table("Good")]
    public class Good
    {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        
        [Column("name")]
        [Required]
        public string Name { get; set; }
        
        [Column("brand")]
        [Required]
        public string Brand { get; set; }
        
        [Column("price")]
        [Required]
        public double Price { get; set; }
        
        [Column("category")]
        [Required]
        public string Category { get; set; }
        
        [Column("description")]
        [Required]
        public string Description { get; set; }
        
        [Column("sum")]
        [Required]
        public int Sum { get; set; }
        
        [ForeignKey("Bank")] 
        [Column("bank_id")]
        public int ShopId { get; set; }
        public Shops.Shop Shop { get; set; }
    }
}

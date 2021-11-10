using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KpiPractice.Core.Shops;

namespace KpiPractice.Data.Shops
{
    [Table("Shop-onion")]
    public class Shop
    {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("count")]
        public int Count { get; set; }

        public virtual ICollection<Core.Goods.Goods> Clients { get; set; }
    }
}

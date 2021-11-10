using System;
using System.ComponentModel.DataAnnotations;

namespace KpiPractice.Orchestrators.Shops
{
    public class UpdateCount
    {
        [Required] 
        public int Count { get; set; }
    }
}

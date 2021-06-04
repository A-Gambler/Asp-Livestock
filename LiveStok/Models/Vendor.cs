using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace livestock.Models
{
    public class Vendor
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }

        public List<StockPurchase> StockPurchase { get; set; }
    }
}

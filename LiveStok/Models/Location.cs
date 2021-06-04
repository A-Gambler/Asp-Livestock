using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace livestock.Models
{
    public class Location
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool Hide { get; set; }

        public List<StockPurchase> StockPurchase { get; set; }
    }
}

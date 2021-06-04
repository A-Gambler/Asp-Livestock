using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using livestock.Models;

namespace livestock.Models
{
    public class BuyType
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }

        virtual public List<StockPurchase> StockPurchases { get; set; }
    }
}





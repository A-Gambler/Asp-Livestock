using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace livestock.Models
{
    public class TypeOfAnimal
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }

        [Display(Name = "Stock Purchase")]
        public List<StockPurchase> StockPurchase { get; set; }
        //public List<PriceXWeight> PriceXWeights { get; set; }
        [Display(Name = "Private Buy")]
        public List<PricePerHeadBuy> PricePerHeadBuys { get; set; }
    }
}

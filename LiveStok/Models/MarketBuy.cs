using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace livestock.Models
{
    [Display(Name = "Market Buy")]
    public class MarketBuy
    {
        [Key]
        public Guid ID { get; set; }
        [Display(Name = "Stock Purchase")]
        public Guid StockPurchaseID { get; set; }
        [Display(Name = "Agent")]
        public Guid? AgentID { get; set; }
        public int? Number { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Price")]
        public Decimal? Price { get; set; }
        [Display(Name = "Desc")]
        public string CodeDesc { get; set; }
        [Display(Name = "Type")]
        public Guid? TypeOfAnimalID { get; set; }

        [Display(Name = "Stock Purchase")]
        public StockPurchase StockPurchase { get; set; }

        [Display(Name = "Type")]
        public TypeOfAnimal TypeOfAnimal { get; set; }
    }
}



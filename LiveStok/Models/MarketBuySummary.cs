using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace livestock.Models
{
    [Display(Name = "Market Buy Summary")]
    public class MarketBuySummary
    {
        [Key]
        public Guid ID { get; set; }
        [Display(Name = "Stock Purchase")]
        public Guid StockPurchaseID { get; set; }
        [Display(Name = "Type")]
        public Guid? TypeOfAnimalID { get; set; }
        [Display(Name = "Desc")]
        public string Description { get; set; }
        public int Number { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Av. Price")]
        public Decimal? AvPrice { get; set; }
        [Display(Name = "Freight")]
        public Decimal? Freight { get; set; }
        [Display(Name = "Skin")]
        public Decimal? Skin { get; set; }
        [Display(Name = "Av. Weight")]
        public Decimal? AvWeight { get; set; }
        [Display(Name = "Cost per/kg")]
        public Decimal? CostXKg { get; set; }

        public TypeOfAnimal TypeOfAnimal { get; set; }
        public StockPurchase stockPurchase { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace livestock.Models
{
    [Display(Name = "Price per head Buy")]
    public class PricePerHeadBuy
    {
        [Key]
        [JsonIgnore]
        public Guid ID { get; set; }
        [Display(Name = "Stock Purchase")]
        [JsonIgnore]
        public Guid StockPurchaseID { get; set; }
        [Display(Name = "Totals")]
        public int? HeadsBought { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Av Price")]
        public Decimal? Price { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Av Skin")]
        public Decimal? Skin { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}", ApplyFormatInEditMode = true)]
        public Decimal? Freight { get; set; }
        [DisplayFormat(DataFormatString = "{0:F1}", ApplyFormatInEditMode = true)]
        [Display(Name = "Av Weight")]
        public Decimal? Weight { get; set; }
        [Display(Name = "Type")]
        public Guid? TypeOfAnimalID { get; set; }

        [Display(Name = "Stock Purchase")]
        [JsonIgnore]
        public StockPurchase StockPurchase { get; set; }

        [Display(Name = "Type")]
        [JsonIgnore]
        public TypeOfAnimal TypeOfAnimal { get; set; }
    }
}



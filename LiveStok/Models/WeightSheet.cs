using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace livestock.Models
{
    public class WeightSheet
    {
        [Key]
        public Guid ID { get; set; }

        public Guid StockPurchaseID { get; set; }
        [Display(Name = "Lot No")]
        public int LotNumber { get; set; }
        public DateTime Date { get; set; }
        public string Origin { get; set; }
        public string Category { get; set; }
        public decimal Skin { get; set; }
        [Display(Name = "Weight Off")]
        public decimal WeightOff { get; set; }

        public List<Weight> Weights { get; set; }
        public StockPurchase StockPurchase { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using LiveStok.Models;

namespace livestock.Models
{
    [Display(Name = "Stock Purchase")]
    public class StockPurchase
    {
        //NOTE!!!!!!  For every new field, an input hidden has to be created in EditDelivered Page

        [Key]
        public Guid ID { get; set; }
        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [Display(Name ="Buyer No")]
        public string BuyerNo { get; set; }
        [Display(Name ="Buyer")]
        public Guid BuyerID { get; set; }
        
        [Display(Name ="Buy Type")]
        public Guid BuyTypeID { get; set; }

        ///Optionals////
        [Display(Name = "Type")]
        public Guid? TypeOfAnimalID { get; set; }
        public int? Number { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Freight { get; set; }
        [Display(Name = "Est. Wgt")]
        [MaxLength(25)]
        public string EstimatedWeight { get; set; }
        [Display(Name = "Agent")]
        ///////

        public Guid AgentID { get; set; }
        [Display(Name = "Location")]
        public Guid LocationID { get; set; }
        [Display(Name = "Vendor")]
        //public Guid VendorID { get; set; }
        public string VendorName { get; set; }
        [Display(Name = "Intended Delivery Date"), DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime IntendedDeliveryDate { get; set; }
        [Display(Name = "Transport")]
        public Guid TransportID { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Actual Delivery Date"),DataType(DataType.Date)]
        public DateTime? DateDelivered { get; set; }
        [Display(Name = "No Delivered.")]
        public int NumberDelivered { get; set; }
        [Display(Name = "YTB Del.")]
        public int YTBDelivered { get; set; }
        [Display(Name = "Invoice Received")]
        public bool InvoiceRecD { get; set; }
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        [Display(Name = "Contact Phone")]
        public string ContactPhone { get; set; }
        [Display(Name = "Contact Email")]
        public string ContactFax { get; set; }
        //public string PurchaseType { get; set; } //Market Buy | Hooks Buy | Price per Head Buy

        [Display(Name = "0.0 - 9.9Kg")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public Decimal? WCl1Price { get; set; }
        [Display(Name = "10.0 - 11.9Kg")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public Decimal? WCl2Price { get; set; }
        [Display(Name = "12.0 - 13.9Kg")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public Decimal? WCl3Price { get; set; }
        [Display(Name = "14.0 - 15.9Kg")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public Decimal? WCl4Price { get; set; }
        [Display(Name = "16.0 - 17.9Kg")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public Decimal? WCl5Price { get; set; }
        [Display(Name = "18.0 - 19.9Kg")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public Decimal? WCl6Price { get; set; }
        [Display(Name = "20.0 - 23.9Kg")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public Decimal? WCl7Price { get; set; }
        [Display(Name = "24.0 - 25.9Kg")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public Decimal? WCl8Price { get; set; }
        [Display(Name = "26.0 - 29.9Kg")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public Decimal? WCl9Price { get; set; }
        [Display(Name = "30.0 - 31.9Kg")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public Decimal? WCl10Price { get; set; }
        [Display(Name = "32.0 - 34.9Kg")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public Decimal? WCl11Price { get; set; }
        [Display(Name = "35.0 - 80.9Kg")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public Decimal? WCl12Price { get; set; }
        //public Guid? PricePerHeadBuyID { get; set; }

        public Buyer Buyer { get; set; }
        [Display(Name = "Type")]
        public TypeOfAnimal TypeOfAnimal { get; set; }
        public Agent Agent { get; set; }
        public Location Location { get; set; }
        //public Vendor Vendor { get; set; }
        public Transport Transport { get; set; }

        [Display(Name = "Weight Sheet")]
        public WeightSheet WeightSheet { get; set; }

        [Display(Name = "Price per Head Buy")]
        public List<PricePerHeadBuy> PricePerHeadBuys { get; set; }

        [Display(Name = "Market Buy")]
        public List<MarketBuy> MarketBuys { get; set; }

        [Display(Name = "Market Buy Summary")]
        public List<MarketBuySummary> MarketBuySummaries { get; set; }

        [Display(Name = "Buy Type")]
        public BuyType BuyType { get; set; }

    }
}

using livestock.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveStok.Models.NonDBModels
{
    public class PartialStockPurchaseBuyModel
    {
        public StockPurchase StockPurchase { get; set; }
        public List<PricePerHeadBuy> PricePerHeadBuys { get; set; }
        public SelectList TypeOfAnimals_SelectList { get; set; }
        public SelectList Agents_SelectList { get; set; }
        public List<MarketBuy> MarketBuys { get; set; }
    }
}

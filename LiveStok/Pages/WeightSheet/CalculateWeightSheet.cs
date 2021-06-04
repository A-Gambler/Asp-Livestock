using livestock.Models;
using LiveStok.Models.NonDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveStok
{
    public class CalculateWeightSheet
    {

        public static List<Decimal> ListOfWeightsFromText(string WeightTextList, decimal WeightOff)
        {
            var splitted = WeightTextList.Split(new string[] { "\r\n", " " }, StringSplitOptions.RemoveEmptyEntries);
            List<Decimal> ListWeights = new List<decimal>();
            foreach (string word in splitted)
            {
                if (Convert.ToDecimal(word) > 0)
                {
                    ListWeights.Add(Convert.ToDecimal(word) - WeightOff);
                }
            }

            return ListWeights;
        }

        public static WeightSheetCalculation Calculate(livestock.Data.ApplicationDbContext _context, Guid StockPurchaseID, string WeightTextList, 
                        decimal WeightOff, decimal SkinPrice)
        {
            

            return Calculate(_context, StockPurchaseID, ListOfWeightsFromText(WeightTextList,WeightOff), SkinPrice);
        }

        public static WeightSheetCalculation Calculate(livestock.Data.ApplicationDbContext _context, Guid StockPurchaseID, List<Decimal> ListWeights, decimal SkinPrice)
        {
            WeightSheetCalculation WeightSheetCalculation = new WeightSheetCalculation();

            WeightSheetCalculation.weightRanges = new List<WeightRange>();

            var StockPurchase = _context.StockPurchase.Where(x => x.ID == StockPurchaseID).FirstOrDefault();

            //List<PriceXWeight> PriceXWeights = _context.PriceXWeights.Where(x => x.TypeOfAnimalID == StockPurchase.TypeOfAnimalID).ToList();

            WeightSheetCalculation.TotalCount = ListWeights.Count();
            WeightSheetCalculation.TotalWeight = ListWeights.Sum();
            WeightSheetCalculation.AverageWeight = WeightSheetCalculation.TotalWeight / WeightSheetCalculation.TotalCount;
            WeightSheetCalculation.AveragePriceXKilo = WeightSheetCalculation.AverageWeight / WeightSheetCalculation.TotalCount;
            WeightSheetCalculation.SkinValue = WeightSheetCalculation.TotalCount * SkinPrice;

            //foreach (var rangePricePerWeight in PriceXWeights)
            for (int i = 1; i <= 12; i++)
            {
                var newRange = new WeightRange();

                switch (i)
                {
                   case 1:
                        newRange.Min = 0.0m;
                        newRange.Max = 9.9m;
                        newRange.Price = StockPurchase.WCl1Price.GetValueOrDefault();
                        break;
                    case 2:
                        newRange.Min = 10.0m;
                        newRange.Max = 11.9m;
                        newRange.Price = StockPurchase.WCl2Price.GetValueOrDefault();
                        break;
                    case 3:
                        newRange.Min = 12.0m;
                        newRange.Max = 13.9m;
                        newRange.Price = StockPurchase.WCl3Price.GetValueOrDefault();
                        break;
                    case 4:
                        newRange.Min = 14.0m;
                        newRange.Max = 15.9m;
                        newRange.Price = StockPurchase.WCl4Price.GetValueOrDefault();
                        break;
                    case 5:
                        newRange.Min = 16.0m;
                        newRange.Max = 17.9m;
                        newRange.Price = StockPurchase.WCl5Price.GetValueOrDefault();
                        break;
                    case 6:
                        newRange.Min = 18.0m;
                        newRange.Max = 19.9m;
                        newRange.Price = StockPurchase.WCl6Price.GetValueOrDefault();
                        break;
                    case 7:
                        newRange.Min = 20.0m;
                        newRange.Max = 23.9m;
                        newRange.Price = StockPurchase.WCl7Price.GetValueOrDefault();
                        break;
                    case 8:
                        newRange.Min = 24.0m;
                        newRange.Max = 25.9m;
                        newRange.Price = StockPurchase.WCl8Price.GetValueOrDefault();
                        break;
                    case 9:
                        newRange.Min = 26.0m;
                        newRange.Max = 29.9m;
                        newRange.Price = StockPurchase.WCl9Price.GetValueOrDefault();
                        break;
                    case 10:
                        newRange.Min = 30.0m;
                        newRange.Max = 31.9m;
                        newRange.Price = StockPurchase.WCl10Price.GetValueOrDefault();
                        break;
                    case 11:
                        newRange.Min = 32.0m;
                        newRange.Max = 34.9m;
                        newRange.Price = StockPurchase.WCl11Price.GetValueOrDefault();
                        break;
                    case 12:
                        newRange.Min = 35.0m;
                        newRange.Max = 80.0m;
                        newRange.Price = StockPurchase.WCl12Price.GetValueOrDefault();
                        break;

                }



                var res = from p in ListWeights
                          where p >= newRange.Min && p <= newRange.Max
                          select p;
                newRange.Count = res.Count();
                newRange.Weight = res.Sum();
                newRange.Value = Math.Round(newRange.Weight * newRange.Price, 2);
                newRange.WeightPercentageOfTotal = Math.Round((newRange.Weight / WeightSheetCalculation.TotalWeight) * 100, 1);
                WeightSheetCalculation.weightRanges.Add(newRange);
            }

            WeightSheetCalculation.weightRanges = WeightSheetCalculation.weightRanges.OrderBy(x => x.Max).ToList();

            WeightSheetCalculation.TotalValue_WithoutSkin = WeightSheetCalculation.weightRanges.Sum(x => x.Value);
            WeightSheetCalculation.AveragePrice = WeightSheetCalculation.TotalValue_WithoutSkin / WeightSheetCalculation.TotalCount;
            WeightSheetCalculation.TotalValue_SkinPlusValue = WeightSheetCalculation.TotalValue_WithoutSkin + WeightSheetCalculation.SkinValue;

            return WeightSheetCalculation;
        }
    }
}

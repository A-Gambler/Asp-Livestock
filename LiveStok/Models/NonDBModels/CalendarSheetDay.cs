using livestock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveStok.Models.NonDBModels
{
    public class CalendarSheetDay
    {
        public List<CalendarSheetDay_StockPurchaseRow> StockPurchasesRow { get; set; }
        public List<AnimalQuantity> animalQuantityDayTotals { get; set; }
    }

    public class CalendarSheetDay_StockPurchaseRow
    {
        public StockPurchase StockPurchase { get; set; }
        public List<AnimalQuantity> animalQuantityRowTotals { get; set; }
    }

    public class AnimalQuantity
    {
        public string AnimalName { get; set; }
        public int Quantity { get; set; }
    }
}

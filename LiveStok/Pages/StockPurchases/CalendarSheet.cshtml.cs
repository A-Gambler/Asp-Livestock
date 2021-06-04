using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;
using LiveStok.Models.NonDBModels;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace livestock.Pages.StockPurchasesPage
{
    public class CalendarSheetModel : PageModel
    {

        private readonly livestock.Data.ApplicationDbContext _context;

        public IList<CalendarSheetData> CalendarSheetDataList { get; set; }
        public List<AnimalQuantity> AnimalQuantityMonthTotals { get; set; }

        public CalendarSheetModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }


    

        public async Task OnGetAsync()
        {

            this.CalendarSheetDataList = new List<CalendarSheetData>();

            //var preRes = await _context.StockPurchase.Where(x=> x.DateDelivered.HasValue == false)
            //    .Include(s => s.Agent)
            //    .Include(s => s.Buyer)
            //    .Include(s => s.Location)
            //    .Include(s => s.Transport)
            //    .Include(s => s.TypeOfAnimal)
            //    .Include(s => s.Vendor)
            //    .Include(s=> s.MarketBuys)
            //    .Include(s=> s.PricePerHeadBuys)

            //    .GroupBy(x=> x.IntendedDeliveryDate.Date )
            //    .OrderBy(x=> x.Key)
            //    .ToListAsync();

            var prepreRes = await _context.StockPurchase.Where(x => x.DateDelivered.HasValue == false &&
                                                            x.IntendedDeliveryDate >= DateTime.Now.Date &&
                                                            x.IntendedDeliveryDate <= DateTime.Now.AddDays(30).Date)
                                                .Include(s => s.Agent)
                                                .Include(s => s.Buyer)
                                                .Include(s => s.Location)
                                                .Include(s => s.Transport)
                                                .Include(s => s.TypeOfAnimal)
                                                //.Include(s => s.Vendor)
                                                .Include(s => s.MarketBuys)
                                                .Include(s => s.PricePerHeadBuys)
                                                .ToListAsync();

            //var prepreRes = await _context.StockPurchase.Where(x => x.DateDelivered.HasValue == false
            //                                             )
            //   .Include(s => s.Agent)
            //   .Include(s => s.Buyer)
            //   .Include(s => s.Location)
            //   .Include(s => s.Transport)
            //   .Include(s => s.TypeOfAnimal)
            //   .Include(s => s.Vendor)
            //   .Include(s => s.MarketBuys)
            //   .Include(s => s.PricePerHeadBuys)
            //   .ToListAsync();

            var preRes = prepreRes
                         .GroupBy(x => x.IntendedDeliveryDate.Date)
                            .OrderBy(x => x.Key);

            var Animals = await _context.TypeOfAnimals.ToListAsync(); 

            foreach (var item in preRes)
            {

                var CalendarSheetDay = new CalendarSheetDay()
                {
                    StockPurchasesRow = new List<CalendarSheetDay_StockPurchaseRow>() 
                };

                List<AnimalQuantity> animalsQuantity_OfDay = new List<AnimalQuantity>();

                foreach (StockPurchase purchase in item)
                {
                    List<AnimalQuantity> animalsQuantity_OfRow = new List<AnimalQuantity>();

                    switch (purchase.BuyTypeID.ToString())
                    {
                        case "29dec9c6-c2c3-4603-9c37-3256ab99215a"://Market Buy
                            animalsQuantity_OfRow = (from p in purchase.MarketBuys
                                    group p by p.TypeOfAnimalID into g
                                    select new AnimalQuantity { AnimalName = Animals.Where(m=> m.ID == g.Key).First().Name,
                                                                    Quantity = g.Sum(m => m.Number).GetValueOrDefault() }).ToList();
                            break;
                        case "d180ff28-1321-467a-a0de-d7955d463762"://Private Buy
                            animalsQuantity_OfRow = (from p in purchase.PricePerHeadBuys
                                                     group p by p.TypeOfAnimalID into g
                                                     select new AnimalQuantity
                                                     {
                                                         AnimalName = Animals.Where(m => m.ID == g.Key).First().Name,
                                                         Quantity = g.Sum(m => m.HeadsBought ).GetValueOrDefault()
                                                     }).ToList();
                            break;
                        case "196d17bd-5320-47da-9d44-57ff705c18b9"://Hooks Buy
                            //animalsQuantity_OfRow.Add( new AnimalQuantity() { AnimalName = Animals.Where(m => m.ID == purchase.TypeOfAnimalID).First().Name,
                            //                                                 Quantity = purchase.w});
                            animalsQuantity_OfRow.Add(new AnimalQuantity()
                            {
                                AnimalName = purchase.TypeOfAnimal.Name,
                                Quantity = purchase.Number.GetValueOrDefault()
                            });
                            break;
                    }

                    CalendarSheetDay.StockPurchasesRow.Add(new CalendarSheetDay_StockPurchaseRow()
                    {
                        StockPurchase = purchase,
                        animalQuantityRowTotals = animalsQuantity_OfRow
                    });

                    foreach(var AnimalItem in animalsQuantity_OfRow)
                    {
                        var AnimalInCollection = animalsQuantity_OfDay.Where(x => x.AnimalName == AnimalItem.AnimalName).FirstOrDefault();

                        if (AnimalInCollection != null)
                        {
                            AnimalInCollection.Quantity += AnimalItem.Quantity;
                        }
                        else
                        {
                            animalsQuantity_OfDay.Add(new AnimalQuantity()
                            {
                                AnimalName = AnimalItem.AnimalName,
                                Quantity = AnimalItem.Quantity
                            });
                        }
                    }

                }

                //Add Total File
                if (animalsQuantity_OfDay.Count > 0)
                {
                    animalsQuantity_OfDay.Add(new AnimalQuantity() { AnimalName = "TOTAL", Quantity = animalsQuantity_OfDay.Sum(x => x.Quantity) });
                }

             
             
                CalendarSheetDay.animalQuantityDayTotals = animalsQuantity_OfDay.OrderBy(x=> x.AnimalName).ToList();

                this.CalendarSheetDataList.Add(new CalendarSheetData() { Date = item.Key,
                                                                        calendarSheetDay = CalendarSheetDay
                                                });
            }


            //Calculate Totals of the Month
            this.AnimalQuantityMonthTotals = new List<AnimalQuantity>();
            foreach (var Row in this.CalendarSheetDataList)
            {
                if (Row.calendarSheetDay.animalQuantityDayTotals != null && Row.calendarSheetDay.animalQuantityDayTotals.Count > 0)
                {
                    AnimalQuantityMonthTotals.AddRange(Row.calendarSheetDay.animalQuantityDayTotals);
                }
            }

            this.AnimalQuantityMonthTotals = (from p in AnimalQuantityMonthTotals
                           group p by p.AnimalName into k
                           select new AnimalQuantity() { AnimalName = k.Key, Quantity = k.Sum(x => x.Quantity) }).ToList();

            this.AnimalQuantityMonthTotals = this.AnimalQuantityMonthTotals.OrderBy(x => x.AnimalName).ToList();
            /////////////////////////  
        }
    }
}

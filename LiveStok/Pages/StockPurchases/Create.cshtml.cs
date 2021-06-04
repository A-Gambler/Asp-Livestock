using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using livestock.Data;
using livestock.Models;
using Microsoft.AspNetCore.Authorization;
using System.Transactions;

namespace livestock.Pages.StockPurchasesPage
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;
        
        [BindProperty]
        public StockPurchase StockPurchase { get; set; }

        [BindProperty]
        public List<PricePerHeadBuy> PricePerHeadBuys { get; set; }

        [BindProperty]
        public List<MarketBuy> MarketBuys { get; set; }

        [BindProperty]
        public List<Decimal> Weight { get; set; }

        public enum eType {
            LAMB,
            RAM,
            MUTTON
        };
        
        [BindProperty]
        public eType Type { get; set; }

        public CreateModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {

            LoadComboBoxes();

            if(StockPurchase == null)
            {
                StockPurchase = new StockPurchase();
                PricePerHeadBuys = new List<PricePerHeadBuy>();
                for(int i =0; i <= 12; i++)
                {
                    PricePerHeadBuys.Add(new PricePerHeadBuy()
                    {
                        ID = Guid.NewGuid()
                    });
                }
                MarketBuys = new List<MarketBuy>();
                for (int i = 0; i <= 50; i++)
                {
                    MarketBuys.Add(new MarketBuy()
                    {
                        ID = Guid.NewGuid()
                    });
                }

                StockPurchase.Date = DateTime.Now;
                StockPurchase.IntendedDeliveryDate = DateTime.Now;
            }

            return Page();
        }


         public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return OnGet();
            }

           

            StockPurchase.ID = Guid.NewGuid();

            //using (var myTs = new TransactionScope())
            //{
            //string PurchaseType = ""; //Market Buy | Hooks Buy | Price per Head Buy

            var animalTypes = _context.TypeOfAnimals.ToList();


            switch (StockPurchase.BuyTypeID.ToString())
            {
                case "d180ff28-1321-467a-a0de-d7955d463762": // "Private Buy"

                    foreach (var item in this.PricePerHeadBuys)
                    {
                        if (item.HeadsBought > 0)
                        {
                            //PurchaseType = "Price per Head Buy";
                            item.ID = Guid.NewGuid();
                            item.StockPurchaseID = StockPurchase.ID;
                            _context.PricePerHeadBuy.Add(item);
                        }
                    }

                    StockPurchase.Number = this.PricePerHeadBuys.Sum(x => x.HeadsBought);

                    break;

                case "29dec9c6-c2c3-4603-9c37-3256ab99215a": // "Market Buy"
                    foreach (var item in this.MarketBuys)
                    {
                        if (item.AgentID.HasValue == true && item.Number.GetValueOrDefault() > 0 && item.Price.GetValueOrDefault() > 0 &&
                            string.IsNullOrWhiteSpace(item.CodeDesc) == false)
                        {
                            //PurchaseType = "Market Buy";
                            item.ID = Guid.NewGuid();
                            item.TypeOfAnimalID = GetTypeOfAnimalID(item.CodeDesc);
                            item.StockPurchaseID = StockPurchase.ID;
                            _context.MarketBuys.Add(item);
                        }
                    }



                    foreach (var item in StockPurchase.MarketBuySummaries)
                    {
                        item.ID = new Guid();
                        item.TypeOfAnimalID = GetTypeOfAnimalID(item.Description);
                        item.StockPurchaseID = StockPurchase.ID;
                        _context.MarketBuySummaries.Add(item);
                    }

                    StockPurchase.Number = StockPurchase.MarketBuySummaries.Sum(x => x.Number);

                    break;


                case "196d17bd-5320-47da-9d44-57ff705c18b9": // "Hooks Buy"
                //    if (StockPurchase.WCl1Price.HasValue || StockPurchase.WCl2Price.HasValue ||
                //StockPurchase.WCl3Price.HasValue || StockPurchase.WCl4Price.HasValue ||
                //StockPurchase.WCl5Price.HasValue || StockPurchase.WCl6Price.HasValue ||
                //StockPurchase.WCl6Price.HasValue || StockPurchase.WCl7Price.HasValue ||
                //StockPurchase.WCl8Price.HasValue || StockPurchase.WCl9Price.HasValue ||
                //StockPurchase.WCl10Price.HasValue || StockPurchase.WCl11Price.HasValue ||
                //StockPurchase.WCl12Price.HasValue)
                //    {

                //    }
                    break;

                default:
                    ModelState.AddModelError(string.Empty, "No known Buy Type was selected.");
                    return OnGet();
                    break;
            }




            if (StockPurchase.Number.HasValue && StockPurchase.Number.Value > 0)
            {
                StockPurchase.YTBDelivered = StockPurchase.Number.Value - StockPurchase.NumberDelivered;
            }



            //if (string.IsNullOrWhiteSpace(PurchaseType) == true)
            //{
            //    ModelState.AddModelError(string.Empty, "On of the Type of Purchases (Bottom of the Page) should be filled.");
            //    return OnGet();
            //}

            //StockPurchase.PurchaseType = PurchaseType;
            _context.StockPurchase.Add(StockPurchase);
                await _context.SaveChangesAsync();
                // _context.SaveChanges();

                //myTs.Complete();
            //}

            

            return RedirectToPage("./Index");
        }

        private Guid GetTypeOfAnimalID(string CodeDesc)
        {
            var animalTypes = _context.TypeOfAnimals.ToList();

            Guid TypeOfAnimalID = new Guid();

            switch (CodeDesc)
            {
                case "1":
                    TypeOfAnimalID = animalTypes.Where(x => x.Name == "MUTTON 1").Select(m => m.ID).FirstOrDefault();
                    break;
                case "2":
                    TypeOfAnimalID = animalTypes.Where(x => x.Name == "MUTTON 2").Select(m => m.ID).FirstOrDefault();
                    break;
                case "5":
                    TypeOfAnimalID = animalTypes.Where(x => x.Name == "MUTTON 5").Select(m => m.ID).FirstOrDefault();
                    break;
                case "6":
                    TypeOfAnimalID = animalTypes.Where(x => x.Name == "MUTTON 6").Select(m => m.ID).FirstOrDefault();
                    break;
                case "3":
                    TypeOfAnimalID = animalTypes.Where(x => x.Name == "2 TOOTHS").Select(m => m.ID).FirstOrDefault();
                    break;
                case "8":
                    TypeOfAnimalID = animalTypes.Where(x => x.Name == "LAMBS").Select(m => m.ID).FirstOrDefault();
                    break;
                case "l":
                case "L":
                    TypeOfAnimalID = animalTypes.Where(x => x.Name == "LAMBS").Select(m => m.ID).FirstOrDefault();
                    break;
                case "r":
                case "R":
                    TypeOfAnimalID = animalTypes.Where(x => x.Name == "RAMS").Select(m => m.ID).FirstOrDefault();
                    break;
            }

            return TypeOfAnimalID;
        }


        private void LoadComboBoxes()
        {
            ViewData["AgentID"] = new SelectList(_context.Agent.Where(x=> x.Hide == false).OrderBy(m => m.Name), "ID", "Name");
            ViewData["BuyerID"] = new SelectList(_context.Buyer.Where(x => x.Hide == false).OrderBy(m => m.Code), "ID", "Code");
            ViewData["LocationID"] = new SelectList(_context.Locationts.Where(x => x.Hide == false).OrderBy(m => m.Name), "ID", "Name");
            ViewData["TransportID"] = new SelectList(_context.Transport.Where(x => x.Hide == false).OrderBy(m => m.Name), "ID", "Name");
            ViewData["TypeOfAnimalID"] = new SelectList(_context.TypeOfAnimals.OrderBy(x=> x.Name), "ID", "Name");
            ViewData["VendorID"] = new SelectList(_context.Vendor, "ID", "Name");
            ViewData["BuyTypeID"] = new SelectList(_context.BuyTypes, "ID", "Name");
        }
    }
}
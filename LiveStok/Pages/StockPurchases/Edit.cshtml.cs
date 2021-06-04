using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;

namespace livestock.Pages.StockPurchasesPage
{
    public class EditModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public EditModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StockPurchase StockPurchase { get; set; }

        [BindProperty]
        public List<PricePerHeadBuy> PricePerHeadBuys { get; set; }


        [BindProperty]
        public List<MarketBuy> MarketBuys { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StockPurchase = await _context.StockPurchase
                .Include(s => s.Agent)
                .Include(s => s.Buyer)
                .Include(s => s.Location)
                .Include(s => s.Transport)
                .Include(s => s.TypeOfAnimal)
                .Include(s => s.MarketBuySummaries)
                .Include(s => s.BuyType)
                //.Include(s => s.Vendor)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (StockPurchase == null)
            {
                return NotFound();
            }

            this.PricePerHeadBuys = await _context.PricePerHeadBuy.Where(x => x.StockPurchaseID == StockPurchase.ID).Include(m => m.TypeOfAnimal).ToListAsync();
            this.MarketBuys = await _context.MarketBuys.Where(x => x.StockPurchaseID == StockPurchase.ID).Include(m => m.TypeOfAnimal).ToListAsync();



           ViewData["AgentID"] = new SelectList(_context.Agent.Where(x => x.Hide == false).OrderBy(m => m.Name), "ID", "Name");
           ViewData["BuyerID"] = new SelectList(_context.Buyer.Where(x => x.Hide == false).OrderBy(m => m.Code), "ID", "Code");
           ViewData["LocationID"] = new SelectList(_context.Locationts.Where(x => x.Hide == false).OrderBy(m => m.Name), "ID", "Name");
           ViewData["TransportID"] = new SelectList(_context.Transport.Where(x => x.Hide == false).OrderBy(m => m.Name), "ID", "Name");
           ViewData["TypeOfAnimalID"] = new SelectList(_context.TypeOfAnimals, "ID", "Name");
           //ViewData["VendorID"] = new SelectList(_context.Vendor, "ID", "Name");
            ViewData["BuyTypeID"] = new SelectList(_context.BuyTypes, "ID", "Name");




            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (StockPurchase.Number.HasValue && StockPurchase.Number.Value > 0)
            {
                StockPurchase.YTBDelivered = StockPurchase.Number.Value - StockPurchase.NumberDelivered;
            }
            else
            {
                StockPurchase.YTBDelivered = 0;
            }

            if (StockPurchase.BuyTypeID.ToString() == "d180ff28-1321-467a-a0de-d7955d463762") // "Private Buy"
            {
                foreach (var item in this.PricePerHeadBuys)
                {
                    var DBItem = _context.PricePerHeadBuy.Where(x => x.ID == item.ID).FirstOrDefault();
                    DBItem.HeadsBought = item.HeadsBought;
                    DBItem.Price = item.Price;
                    DBItem.Skin = item.Skin;
                    DBItem.TypeOfAnimalID = item.TypeOfAnimalID;
                    DBItem.Weight = item.Weight;
                    DBItem.Freight = item.Freight;
                    await _context.SaveChangesAsync();
                }
            }

            var animalTypes = _context.TypeOfAnimals.ToList();

            if (StockPurchase.BuyTypeID.ToString() == "29dec9c6-c2c3-4603-9c37-3256ab99215a") // "Market Buy"
            {
                foreach (var item in this.MarketBuys)
                {
                    var DBItem = _context.MarketBuys.Where(x => x.ID == item.ID).FirstOrDefault();
                    DBItem.AgentID = item.AgentID;

                    if(DBItem.CodeDesc != item.CodeDesc)
                    {
                        DBItem.CodeDesc = item.CodeDesc;
                        DBItem.TypeOfAnimalID = GetTypeOfAnimalID(item.CodeDesc);
                    }
                    
                    DBItem.Number = item.Number;
                    DBItem.Price = item.Price;
                    
                    await _context.SaveChangesAsync();
                }
                foreach (var item in StockPurchase.MarketBuySummaries)
                {
                    var MBSItem = _context.MarketBuySummaries.Where(s => s.ID == item.ID).FirstOrDefault();
                    MBSItem.TypeOfAnimalID = GetTypeOfAnimalID(item.Description);
                    MBSItem.AvPrice = item.AvPrice;
                    MBSItem.AvWeight = item.AvWeight;
                    MBSItem.CostXKg = item.CostXKg;
                    MBSItem.Description = item.Description;
                    MBSItem.Number = item.Number;
                    MBSItem.Skin = item.Skin;
                    MBSItem.Freight = item.Freight;
                    await _context.SaveChangesAsync();
                }
            }
            //_context.Attach(StockPurchase).State = EntityState.Modified;
            _context.Entry(StockPurchase).State = EntityState.Modified;//A.L. me daba error asi que agregue actualiza el modelo completo

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockPurchaseExists(StockPurchase.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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


        private bool StockPurchaseExists(Guid id)
        {
            return _context.StockPurchase.Any(e => e.ID == id);
        }
    }
}

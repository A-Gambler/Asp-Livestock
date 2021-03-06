using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace livestock.Pages.StockPurchasesPage
{
    public class DeleteModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public DeleteModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public LiveStok.Models.NonDBModels.PartialStockPurchaseBuyModel PartialStockPurchaseBuyModel { get; set; }

        [BindProperty]
        public StockPurchase StockPurchase { get; set; }

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
                //.Include(s => s.Vendor)
                .Include(s=> s.MarketBuySummaries)
                .Include(s=> s.BuyType).FirstOrDefaultAsync(m => m.ID == id);

            if (StockPurchase == null)
            {
                return NotFound();
            }

            PartialStockPurchaseBuyModel = new LiveStok.Models.NonDBModels.PartialStockPurchaseBuyModel()
            {
                StockPurchase = StockPurchase,
                PricePerHeadBuys = await _context.PricePerHeadBuy.Where(x => x.StockPurchaseID == StockPurchase.ID).Include(m => m.TypeOfAnimal).ToListAsync(),
                TypeOfAnimals_SelectList = new SelectList(_context.TypeOfAnimals, "ID", "Name"),
                Agents_SelectList = new SelectList(_context.Agent, "ID", "Name"),
                MarketBuys = await _context.MarketBuys.Where(x => x.StockPurchaseID == StockPurchase.ID).Include(m => m.TypeOfAnimal).ToListAsync()
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StockPurchase = await _context.StockPurchase.FindAsync(id);

            if (StockPurchase != null)
            {
                _context.StockPurchase.Remove(StockPurchase);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

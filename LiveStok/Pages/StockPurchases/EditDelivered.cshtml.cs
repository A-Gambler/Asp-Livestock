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
    public class EditDelivered : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public EditDelivered(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
                .FirstOrDefaultAsync(m => m.ID == id);

            if (StockPurchase == null)
            {
                return NotFound();
            }
           ViewData["AgentID"] = new SelectList(_context.Agent.Where(x => x.Hide == false).OrderBy(m=> m.Name), "ID", "Name");
           ViewData["BuyerID"] = new SelectList(_context.Buyer.Where(x => x.Hide == false).OrderBy(m => m.Code), "ID", "Code");
           ViewData["LocationID"] = new SelectList(_context.Locationts.Where(x => x.Hide == false).OrderBy(m => m.Name), "ID", "Name");
           ViewData["TransportID"] = new SelectList(_context.Transport.Where(x => x.Hide == false).OrderBy(m => m.Name), "ID", "Name");
           ViewData["TypeOfAnimalID"] = new SelectList(_context.TypeOfAnimals, "ID", "Name");
           //ViewData["VendorID"] = new SelectList(_context.Vendor, "ID", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(StockPurchase).State = EntityState.Modified;

            if (StockPurchase.Number.HasValue && StockPurchase.Number.Value> 0)
            {
                StockPurchase.YTBDelivered = StockPurchase.Number.Value - StockPurchase.NumberDelivered;
            }
            else
            {
                StockPurchase.YTBDelivered = 0;
            }

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

            return RedirectToPage("./Summarized");
        }

        private bool StockPurchaseExists(Guid id)
        {
            return _context.StockPurchase.Any(e => e.ID == id);
        }
    }
}

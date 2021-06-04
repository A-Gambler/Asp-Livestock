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

namespace LiveStok.Pages.borrame
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
                .Include(s => s.Vendor).FirstOrDefaultAsync(m => m.ID == id);

            if (StockPurchase == null)
            {
                return NotFound();
            }
           ViewData["AgentID"] = new SelectList(_context.Agent, "ID", "ID");
           ViewData["BuyerID"] = new SelectList(_context.Buyer, "ID", "ID");
           ViewData["LocationID"] = new SelectList(_context.Locationts, "ID", "ID");
           ViewData["TransportID"] = new SelectList(_context.Transport, "ID", "ID");
           ViewData["TypeOfAnimalID"] = new SelectList(_context.TypeOfAnimals, "ID", "ID");
           ViewData["VendorID"] = new SelectList(_context.Vendor, "ID", "ID");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(StockPurchase).State = EntityState.Modified;

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

        private bool StockPurchaseExists(Guid id)
        {
            return _context.StockPurchase.Any(e => e.ID == id);
        }
    }
}

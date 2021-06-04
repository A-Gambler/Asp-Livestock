using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using livestock.Data;
using livestock.Models;

namespace LiveStok.Pages.borrame
{
    public class CreateModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public CreateModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AgentID"] = new SelectList(_context.Agent, "ID", "ID");
        ViewData["BuyerID"] = new SelectList(_context.Buyer, "ID", "ID");
        ViewData["LocationID"] = new SelectList(_context.Locationts, "ID", "ID");
        ViewData["TransportID"] = new SelectList(_context.Transport, "ID", "ID");
        ViewData["TypeOfAnimalID"] = new SelectList(_context.TypeOfAnimals, "ID", "ID");
        ViewData["VendorID"] = new SelectList(_context.Vendor, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public StockPurchase StockPurchase { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.StockPurchase.Add(StockPurchase);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
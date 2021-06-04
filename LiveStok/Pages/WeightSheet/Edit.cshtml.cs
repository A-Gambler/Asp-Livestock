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

namespace LiveStok.Pages.WeightSheetPage
{
    public class EditModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public EditModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WeightSheet WeightSheet { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeightSheet = await _context.WeightSheet
                .Include(w => w.StockPurchase).FirstOrDefaultAsync(m => m.ID == id);

            if (WeightSheet == null)
            {
                return NotFound();
            }
           ViewData["StockPurchaseID"] = new SelectList(_context.StockPurchase, "ID", "ID");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(WeightSheet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeightSheetExists(WeightSheet.ID))
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

        private bool WeightSheetExists(Guid id)
        {
            return _context.WeightSheet.Any(e => e.ID == id);
        }
    }
}

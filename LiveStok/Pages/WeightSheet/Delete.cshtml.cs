using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;

namespace LiveStok.Pages.WeightSheetPage
{
    public class DeleteModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public DeleteModel(livestock.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeightSheet = await _context.WeightSheet.FindAsync(id);

            if (WeightSheet != null)
            {
                _context.WeightSheet.Remove(WeightSheet);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

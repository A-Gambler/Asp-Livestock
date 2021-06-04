using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;

namespace LiveStok.Pages.BuyersPage
{
    public class DeleteModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public DeleteModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Buyer Buyer { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Buyer = await _context.Buyer.FirstOrDefaultAsync(m => m.ID == id);

            if (Buyer == null)
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

            Buyer = await _context.Buyer.FindAsync(id);

            if (Buyer != null)
            {
                _context.Buyer.Remove(Buyer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

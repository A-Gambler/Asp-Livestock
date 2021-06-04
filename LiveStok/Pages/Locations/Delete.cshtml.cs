using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;

namespace LiveStok.Pages.LocationsPage
{
    public class DeleteModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public DeleteModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Location Location { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Location = await _context.Locationts.FirstOrDefaultAsync(m => m.ID == id);

            if (Location == null)
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

            Location = await _context.Locationts.FindAsync(id);

            if (Location != null)
            {
                _context.Locationts.Remove(Location);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

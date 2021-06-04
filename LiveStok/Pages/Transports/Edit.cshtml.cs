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

namespace LiveStok.Pages.TransportsPage
{
    public class EditModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public EditModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Transport Transport { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Transport = await _context.Transport.FirstOrDefaultAsync(m => m.ID == id);

            if (Transport == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Transport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransportExists(Transport.ID))
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

        private bool TransportExists(Guid id)
        {
            return _context.Transport.Any(e => e.ID == id);
        }
    }
}

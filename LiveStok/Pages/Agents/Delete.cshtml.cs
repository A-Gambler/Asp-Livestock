using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;

namespace LiveStok.Pages.AgentsPage
{
    public class DeleteModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public DeleteModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Agent Agent { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Agent = await _context.Agent.FirstOrDefaultAsync(m => m.ID == id);

            if (Agent == null)
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

            Agent = await _context.Agent.FindAsync(id);

            if (Agent != null)
            {
                _context.Agent.Remove(Agent);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

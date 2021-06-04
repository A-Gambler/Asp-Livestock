using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;

namespace LiveStok.Pages.OpenInvitationsPage
{
    public class DeleteModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public DeleteModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserOpenInvitation UserOpenInvitation { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserOpenInvitation = await _context.UserOpenInvitations.FirstOrDefaultAsync(m => m.Id == id);

            if (UserOpenInvitation == null)
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

            UserOpenInvitation = await _context.UserOpenInvitations.FindAsync(id);

            if (UserOpenInvitation != null)
            {
                _context.UserOpenInvitations.Remove(UserOpenInvitation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

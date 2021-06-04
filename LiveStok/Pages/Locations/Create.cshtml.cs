using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using livestock.Data;
using livestock.Models;

namespace LiveStok.Pages.LocationsPage
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
            return Page();
        }

        [BindProperty]
        public Location Location { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Locationts.Add(Location);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
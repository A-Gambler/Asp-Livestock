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
    public class DetailsModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public DetailsModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}

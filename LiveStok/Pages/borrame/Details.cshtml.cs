using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;

namespace LiveStok.Pages.borrame
{
    public class DetailsModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public DetailsModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public StockPurchase StockPurchase { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StockPurchase = await _context.StockPurchase
                .Include(s => s.Agent)
                .Include(s => s.Buyer)
                .Include(s => s.Location)
                .Include(s => s.Transport)
                .Include(s => s.TypeOfAnimal)
                .Include(s => s.Vendor).FirstOrDefaultAsync(m => m.ID == id);

            if (StockPurchase == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

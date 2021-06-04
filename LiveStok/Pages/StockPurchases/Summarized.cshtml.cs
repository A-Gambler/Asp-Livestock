using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;

namespace livestock.Pages.StockPurchasesPage
{
    public class Summarized : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public Summarized(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<StockPurchase> StockPurchase { get;set; }

        public async Task OnGetAsync()
        {
            StockPurchase = await _context.StockPurchase
                .Where(x => x.DateDelivered == null || x.InvoiceRecD == false || (x.DateDelivered != null && x.DateDelivered.Value.Date > DateTime.Now.AddDays(-30).Date))
                .Include(s => s.Agent)
                .Include(s => s.Buyer)
                .Include(s => s.Location)
                .Include(s => s.Transport)
                .Include(s => s.TypeOfAnimal)
                .OrderBy(m=> m.Date).ThenBy(n => n.Buyer)
                //.Include(s => s.Vendor)
                .ToListAsync();
        }
    }
}

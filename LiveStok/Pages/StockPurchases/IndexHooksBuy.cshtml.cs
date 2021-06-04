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
    public class IndexHooksBuyModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public IndexHooksBuyModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public bool CheckPendingForWeightSheet { get; set; }

        public IList<StockPurchase> StockPurchase { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            IQueryable<StockPurchase> stockPurchases = _context.StockPurchase;
               

            if (CheckPendingForWeightSheet == true)
            {
                stockPurchases = from p in stockPurchases where p.WeightSheet == null select p;
            }

            StockPurchase = await stockPurchases.Include(s => s.Agent)
                .Include(s => s.Buyer)
                .Include(s => s.Location)
                .Include(s => s.Transport)
                .Include(s => s.TypeOfAnimal)
                //.Include(s => s.Vendor)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return await OnGetAsync();
        }
    }
}

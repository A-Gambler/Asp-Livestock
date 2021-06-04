using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;
using Helpers;

namespace livestock.Pages.StockPurchasesPage
{
    public class IndexModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public IndexModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public PaginatedList<StockPurchase> StockPurchase { get;set; }

        public async Task OnGetAsync()
        {

           StockPurchase= await new Controllers.StockPurchasesAPIController(_context).GetStockPurchaseGridRowsAsync("", 1);
            
        }
    }
}

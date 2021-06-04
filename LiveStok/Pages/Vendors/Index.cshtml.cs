using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;

namespace LiveStok.Pages.VendorsPage
{
    public class IndexModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public IndexModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Vendor> Vendor { get;set; }

        public async Task OnGetAsync()
        {
            Vendor = await _context.Vendor.ToListAsync();
        }
    }
}

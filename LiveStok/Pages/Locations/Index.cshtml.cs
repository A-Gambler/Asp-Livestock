using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;

namespace LiveStok.Pages.LocationsPage
{
    public class IndexModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public IndexModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Location> Location { get;set; }

        public async Task OnGetAsync()
        {
            Location = await _context.Locationts.ToListAsync();
        }
    }
}

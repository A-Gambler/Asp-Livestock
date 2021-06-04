using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;

namespace LiveStok.Pages.WeightSheetPage
{
    public class DetailsModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public LiveStok.Models.NonDBModels.WeightSheetCalculation WeightSheetCalculation { get; set; }
        

        public DetailsModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public WeightSheet WeightSheet { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeightSheet = await _context.WeightSheet
                .Include(w => w.StockPurchase).FirstOrDefaultAsync(m => m.ID == id);

            List<decimal> weights = _context.Weights.Where(x => x.WeightSheetID == WeightSheet.ID).Select(m=> m.weight).ToList();

            WeightSheetCalculation = CalculateWeightSheet.Calculate(_context, WeightSheet.StockPurchase.ID, weights, WeightSheet.Skin);

            if (WeightSheet == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace livestock.Pages
{
    public class IndexModel : PageModel
    {
        public ActionResult OnGet()
        {
            if(User.Identity.IsAuthenticated == true)
            {
                return RedirectToPage("/StockPurchases/Index");
            }

            return Page();
        }
    }
}

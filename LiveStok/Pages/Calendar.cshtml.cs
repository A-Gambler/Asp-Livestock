using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using livestock.Models;
using Newtonsoft.Json;

namespace Proyecto.Pages
{
    public class CalendarModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;

        public CalendarModel(livestock.Data.ApplicationDbContext context)
        {
            _context = context;
            //this.newTarea = new StockPurchase() { ID = Guid.NewGuid() };
        }

      
        //[BindProperty]
        //public Tareas newTarea { get; set; }

        public void OnGet()
        {

        }


        //public async Task<IActionResult> OnPostCreateTareaAsync()
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return Page();
        //    //}

        //    newTarea.Id = Guid.NewGuid();

        //    _context.Tareas.Add(newTarea);
        //    await _context.SaveChangesAsync();

        //    //return RedirectToPage("./Index");
        //    return RedirectToPage("Calendar");
        //}
    }
}
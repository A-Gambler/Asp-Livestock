using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;
using LiveStok.Helpers;
using Microsoft.AspNetCore.Identity;

namespace LiveStok.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;
        UserManager<LiveStok.Helpers.ApplicationUser> _userManager;

        public EditModel(livestock.Data.ApplicationDbContext context, UserManager<LiveStok.Helpers.ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        //public LiveStok.Models.NonDBModels.Users user { get; set; }
        public  ApplicationUser user { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }


            //user = await (from p in _userManager.Users
            //              where p.Id == id
            //         select new LiveStok.Models.NonDBModels.Users
            //         {
            //             Email = p.Email,
            //             EmailConfirmed = p.EmailConfirmed,
            //             IsLocked = p.IsLocked,
            //             IsAdministrator = p.IsAdministrator,
            //             Id = p.Id

            //         }).FirstOrDefaultAsync();

            user = await (from p in _context.Users
                          where p.Id == id
                          select p).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userInDB = await (from p in _context.Users
                          where p.Id == user.Id
                          select p).FirstOrDefaultAsync();

            if(user == null)
            {
                return NotFound();
            }
            else
            {
                userInDB.IsAdministrator = user.IsAdministrator;
                userInDB.IsLocked = user.IsLocked;
            }

            //_context.Attach(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../OpenInvitations/Index");
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}

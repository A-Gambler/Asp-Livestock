using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using livestock.Models;
using Microsoft.AspNetCore.Identity;

namespace LiveStok.Pages.OpenInvitationsPage
{
    public class IndexModel : PageModel
    {
        private readonly livestock.Data.ApplicationDbContext _context;
        UserManager<LiveStok.Helpers.ApplicationUser> _userManager;

        public List<LiveStok.Models.NonDBModels.Users> Users {get; set;}


        public IndexModel(livestock.Data.ApplicationDbContext context, UserManager<LiveStok.Helpers.ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<UserOpenInvitation> UserOpenInvitation { get;set; }

        public async Task OnGetAsync()
        {
            UserOpenInvitation = await _context.UserOpenInvitations.ToListAsync();

            Users = (from p in _userManager.Users
                     select new LiveStok.Models.NonDBModels.Users
                     {
                         Email = p.Email,
                         EmailConfirmed = p.EmailConfirmed,
                         IsLocked = p.IsLocked,
                         IsAdministrator = p.IsAdministrator,
                         Id = p.Id

                     }).ToList();


        }
    }
}

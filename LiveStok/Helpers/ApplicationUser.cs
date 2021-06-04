using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LiveStok.Helpers
{
    //https://adrientorris.github.io/aspnet-core/identity/extend-user-model.html

    //Pasos:
    //1. cambiar el encabezado de ApplicationDbContext a => public class ApplicationDbContext : IdentityDbContext<LiveStok.Helpers.ApplicationUser>
    //2. Todos los UserManager<> y SignInManager<> hay que ponerles el ApplicationUser

    //Codebehind => from p in _context.Users
    //ShareLayout o HTML =>  @inject UserManager<LiveStok.Helpers.ApplicationUser> _userManager;
    //@if((await _userManager.GetUserAsync(User)).IsAdministrator == true)

    public class ApplicationUser : IdentityUser
    {
        public bool IsLocked { get; set; }
        public bool IsAdministrator { get; set; }
    }

    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public AppClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        { }

    //    public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    //    {
    //        var principal = await base.CreateAsync(user);

    //        if (!string.IsNullOrWhiteSpace(user.FirstName))
    //        {
    //            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
    //    new Claim(ClaimTypes.GivenName, user.FirstName)
    //});
    //        }

    //        if (!string.IsNullOrWhiteSpace(user.LastName))
    //        {
    //            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
    //     new Claim(ClaimTypes.Surname, user.LastName),
    //});
    //        }

    //        return principal;
    //    }

    //    //Esto va en ConfigureServices
    //    services.AddScoped<Microsoft.AspNetCore.Identity.IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();

    }
}

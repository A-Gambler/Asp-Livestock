using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;

namespace LiveStok.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<LiveStok.Helpers.ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IEmailSender _emailSender;

        public LoginModel(SignInManager<LiveStok.Helpers.ApplicationUser> signInManager, ILogger<LoginModel> logger, IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public bool ShowReenviarEmailDeConformacion { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                
                else
                {

                    var user = await _signInManager.UserManager.FindByEmailAsync(Input.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError(string.Empty, "Users is not registered");
                    }
                    else if (await _signInManager.UserManager.IsEmailConfirmedAsync(user) == false)
                    {
                        ModelState.AddModelError(string.Empty, "You have to confirm your email. Please verify your inbox email.");
                        ShowReenviarEmailDeConformacion = true;

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public async Task<IActionResult> OnPostReenviarConfirmacionEmailAsync()
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(Input.Email);
            var code = await _signInManager.UserManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                      "/Account/ConfirmEmail",
                      pageHandler: null,
                      values: new { userId = user.Id, code = code },
                      protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(Input.Email, "Confirm email",
                        $"Please confirm your email account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            ModelState.Clear();

            return Page();
        }
    }
}

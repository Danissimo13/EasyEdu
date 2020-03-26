using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirtualSchool.Models;
using VirtualSchool.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace VirtualSchool.Pages
{
    public class ForgotModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        private VSContext db;

        public ForgotModel(VSContext context)
        {
            db = context;
        }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("Acc");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            User user = await db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == Email);

            await Authenticate(user);

            return RedirectToPage("Acc");
        }


        [NonAction]
        private async Task Authenticate(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserId.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.RoleName)
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }
    }
}
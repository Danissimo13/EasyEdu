using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VirtualSchool.Models;
using VirtualSchool.Services;
using VirtualSchool.ViewModels;

namespace VirtualSchool.Pages
{
    public class AuthModel : PageModel
    {
        [BindProperty]
        public AuthViewModel AuthData { get; set; }

        private VSContext db;
        private HashService hashService;

        public AuthModel(VSContext context, HashService hService)
        {
            db = context;
            hashService = hService;
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
            if (ModelState.IsValid)
            {
                User user = await db.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == AuthData.Email);

                if (user == null)
                {
                    ModelState.AddModelError("AuthData.Password", "Аккаунта с таким e-mail не существует");
                    return Page();
                }

                if(!user.IsVerified)
                {
                    return RedirectToPage("NonVerified");
                }

                if (user.Password == hashService.GetHash(AuthData.Password))
                {
                    await Authenticate(user);
                    return RedirectToPage("Acc");
                }
                else
                {
                    ModelState.AddModelError("AuthData.Password", "Пароль неверен");
                    return Page();
                }
            }

            return Page();
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VirtualSchool.Models;
using VirtualSchool.Services;

namespace VirtualSchool.Pages
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class NewPassModel : PageModel
    {
        [BindProperty]
        public string NewPass { get; set; }

        private VSContext db;
        private HashService hashService;

        public NewPassModel(VSContext context, HashService hService)
        {
            db = context;
            hashService = hService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            User profile = await db.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == User.Identity.Name);
            profile.Password = hashService.GetHash(NewPass);
            await db.SaveChangesAsync();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("Auth");
        }
    }
}
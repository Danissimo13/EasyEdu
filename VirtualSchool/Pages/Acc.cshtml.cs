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
    public class AccModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }

        public User Profile { get; set; }

        private UserService userService;

        public AccModel(UserService uService)
        {
            userService = uService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!Id.HasValue)
            {
                Profile = await userService.GetUserAsync(int.Parse(User.Identity.Name));
            }
            else
            {
                Profile = await userService.GetUserAsync(Id.Value);
                if (Profile == null)
                {
                    return RedirectToPage("NotFound");
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostExitAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("Index");
        }
    }
}
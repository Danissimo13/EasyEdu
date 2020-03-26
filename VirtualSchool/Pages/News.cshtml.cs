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

namespace VirtualSchool.Pages
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class NewsModel : PageModel
    {
        public IQueryable<News> News { get; set; }

        private VSContext db;

        public NewsModel(VSContext context)
        {
            db = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            User user = await db.Users.Include(u => u.Class).FirstOrDefaultAsync(u => u.UserId.ToString() == User.Identity.Name);

            if(user == null)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToPage("Auth");
            }

            News = db.News.Where(n => user.Class.SchoolId == n.SchoolId);
            return Page();
        }
    }
}
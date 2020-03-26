using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirtualSchool.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using VirtualSchool.Services;

namespace VirtualSchool.Pages
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class NewsPageModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }

        public News News { get; set; }

        private VSContext db;
        private UserService userService;

        public NewsPageModel(VSContext context, UserService uService)
        {
            db = context;
            userService = uService;
        }

        public async Task<IActionResult> OnGet()
        {
            if(!Id.HasValue)
            {
                return RedirectToPage("News");
            }

            User user = await userService.GetUserAsync(int.Parse(User.Identity.Name));
            News = await db.News.Include(n => n.School).FirstOrDefaultAsync(n => n.NewsId == Id);

            if((News == null) || (News.SchoolId != user.Class.SchoolId))
            {
                return RedirectToPage("NotFound");
            }

            return Page();
        }
    }
}
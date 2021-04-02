using System;
using System.Linq;
using System.Threading.Tasks;
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
    public class DiaryModel : PageModel
    {
        public IQueryable<Day> Days { get; set; }

        private DateService dateService;
        private VSContext db;

        public DiaryModel(VSContext context, DateService dService)
        {
            db = context;
            dateService = dService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await db.Objects.LoadAsync();
            await db.Classes.LoadAsync();
            await db.DayObjects.LoadAsync();

            User user = await db.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == User.Identity.Name);

            DateTime monday = dateService.GetMondayOfWeek();
            DateTime saturday = dateService.GetSaturdayOfWeek();

            Days = db.Days.Where(d => (d.Date.Date >= monday.Date) && (d.Date.Date <= saturday.Date)).Where(d => d.ClassId == user.ClassId);

            return Page();
        }
    }
}
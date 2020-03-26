using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VirtualSchool.Models;
using VirtualSchool.Services;
using VirtualSchool.ViewModels;

namespace VirtualSchool.Pages
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "admin")]
    public class AdminModel : PageModel
    {
        [BindProperty]
        public AddDayViewModel NewDay { get; set; }
        public IQueryable<User> NonVerifiedUsers { get; set; }
        public IQueryable<News> News { get; set; }
        public IQueryable<Day> Week { get; set; }
        public SelectList Objects { get; set; }

        private DateService dateService;
        private VSContext db;

        public AdminModel(VSContext context, DateService dService)
        {
            db = context;
            dateService = dService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await db.Objects.LoadAsync();
            await db.Classes.LoadAsync();
            await db.DayObjects.LoadAsync();

            User admin = await db.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == User.Identity.Name);

            NonVerifiedUsers = db.Users.Where(u => (u.Class.SchoolId == admin.Class.SchoolId) && (u.IsVerified == false));

            DateTime monday = dateService.GetMondayOfWeek();
            DateTime saturday = dateService.GetSaturdayOfWeek();
            Week = db.Days
                .Where(d => (d.Date.Date >= monday.Date) && (d.Date.Date <= saturday.Date))
                .Where(d => d.ClassId == admin.ClassId)
                .Include(d => d.DayObjects);
            Objects = new SelectList(db.Objects, "ObjectName", "ObjectName");

            News = db.News.Where(n => n.SchoolId == admin.Class.SchoolId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            User admin = await db.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == User.Identity.Name);

            DateTime monday = dateService.GetMondayOfWeek();
            DateTime saturday = dateService.GetSaturdayOfWeek();

            Week = db.Days
                .Where(d => (d.Date.Date >= monday.Date) && (d.Date.Date <= saturday.Date))
                .Where(d => d.ClassId == admin.ClassId)
                .Include(d => d.DayObjects);

            Day day = await Week.FirstOrDefaultAsync(d => d.DayNumber == NewDay.DayNumber);
            if (day == null)
            {
                day = new Day()
                {
                    ClassId = admin.ClassId,
                    Date = monday.AddDays(NewDay.DayNumber - 1),
                    DayNumber = NewDay.DayNumber,
                };
                SetObjectsOnDay(day);

                await db.Days.AddAsync(day);
            }
            else
            {
                ReplaceObjectsOnDay(day);
            }

            await db.SaveChangesAsync();
            return RedirectToPage("Admin");
        }

        [NonAction]
        private void SetObjectsOnDay(Day day)
        {
            Models.Object first = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.FirstObj);
            Models.Object second = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.SecondObj);
            Models.Object third = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.ThirdObj);
            Models.Object fourth = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.FourthObj);
            Models.Object fifth = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.FifthObj);
            Models.Object sixth = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.SixthObj);
            Models.Object seventh = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.SeventhObj);
            Models.Object eighth = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.EighthObj);

            day.AddObject(first);
            day.AddObject(second);
            day.AddObject(third);
            day.AddObject(fourth); 
            day.AddObject(fifth);
            day.AddObject(sixth);
            day.AddObject(seventh);
            day.AddObject(eighth);
        }

        [NonAction]
        private void ReplaceObjectsOnDay(Day day)
        {
            Models.Object first = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.FirstObj);
            Models.Object second = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.SecondObj);
            Models.Object third = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.ThirdObj);
            Models.Object fourth = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.FourthObj);
            Models.Object fifth = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.FifthObj); 
            Models.Object sixth = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.SixthObj); 
            Models.Object seventh = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.SeventhObj); 
            Models.Object eighth = db.Objects.FirstOrDefault(o => o.ObjectName == NewDay.EighthObj);

            day.AddChange(1, first);
            day.AddChange(2, second);
            day.AddChange(3, third);
            day.AddChange(4, fourth);
            day.AddChange(5, fifth);
            day.AddChange(6, sixth);
            day.AddChange(7, seventh);
            day.AddChange(8, eighth);
        }
    }
}
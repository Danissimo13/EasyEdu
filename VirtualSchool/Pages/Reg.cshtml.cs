using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VirtualSchool.Models;
using VirtualSchool.Services;
using VirtualSchool.ViewModels;

namespace VirtualSchool.Pages
{
    public class RegModel : PageModel
    {
        [BindProperty]
        public RegViewModel RegData { get; set; }

        private VSContext db;
        private HashService hashService;
        private UserService userService;

        public RegModel(VSContext context, HashService hService, UserService uService)
        {
            db = context;
            hashService = hService;
            userService = uService;
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
                School userSchool = await db.Schools.Include(s => s.Classes).FirstOrDefaultAsync(s => s.SchoolNumber == RegData.SchoolNumber);
                if(userSchool == null)
                {
                    ModelState.AddModelError("RegData.SchoolNumber", "Такая школа не зарегистрирована в нашей системе.");
                    return Page();
                }

                Class userClass = userSchool.Classes.FirstOrDefault(c => ((c.ClassNumber == RegData.ClassNumber) && (c.ClassChar.ToLower() == RegData.ClassChar.ToLower())));
                if(userClass == null)
                {
                    ModelState.AddModelError("RegData.ClassNumber", "Такой класс не зарегистрирован в нашей системе.");
                    return Page();
                }

                User newUser = new User()
                {
                    Email = RegData.Email,
                    FirstName = RegData.FirstName,
                    LastName = RegData.LastName,
                    Class = userClass,
                    Password = hashService.GetHash(RegData.Password),
                    Role = db.Roles.First(),
                    IsVerified = false
                };

                bool isAdded = await userService.TryAddUserAsync(newUser);
                if (!isAdded)
                {
                    ModelState.AddModelError("RegData.Email", "Пользователь с таким Email уже существует.");
                    return Page();
                }

                return RedirectToPage("NonVerified");
            }

            return Page();
        }
    }
}
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
    public class ChatroomModel : PageModel
    {
        public List<Message> Messages { get; set; }

        private VSContext db;
        private UserService userService;

        public ChatroomModel(VSContext context, UserService uService)
        {
            db = context;
            userService = uService;
        }

        public async Task<IActionResult> OnGet()
        {
            User user = await userService.GetUserAsync(int.Parse(User.Identity.Name));

            Messages = await db.Messages.Include(m => m.Author).ThenInclude(u => u.Class).Where(m => m.Author.Class.SchoolId == user.Class.SchoolId).ToListAsync();
            Messages.Sort((f, s) => f.MessageId > s.MessageId ? 1 : -1);

            return Page();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VirtualSchool.Models;
using VirtualSchool.Services;

namespace VirtualSchool.Pages
{
    public class PersonalChatModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }

        public User Companion { get; set; } //С кем идёт переписка
        public List<PMessage> Messages { get; set; }

        private VSContext db;
        private UserService userService;

        public PersonalChatModel(VSContext context, UserService uService)
        {
            db = context;
            userService = uService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!Id.HasValue)
            {
                return RedirectToPage("NotFound");
            }

            Companion = await userService.GetUserAsync(Id.Value);

            if(Companion == null)
            {
                return RedirectToPage("NotFound");
            }

            Messages = await db.PMessages.Where(m => ((m.RecipientId.ToString() == User.Identity.Name) && (m.AuthorId == Companion.UserId)) ||
                                                     ((m.RecipientId == Companion.UserId) && (m.AuthorId.ToString() == User.Identity.Name))).ToListAsync();
            Messages.Sort((f, s) => f.PMessageId > s.PMessageId ? 1 : -1);

            return Page();
        }
    }
}
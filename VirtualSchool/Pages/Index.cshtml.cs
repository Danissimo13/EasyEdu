using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VirtualSchool.Models;

namespace VirtualSchool.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private VSContext _db;

        public IndexModel(ILogger<IndexModel> logger, VSContext db)
        {
            _logger = logger;
            _db = db;
        }

        public void OnGet()
        {
            _db.Users.Include(u => u.Class).Include(u => u.Role);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VirtualSchool.Models;

namespace VirtualSchool.Pages
{
    public class PmModel : PageModel
    {
        public User Companion { get; set; } //С кем идёт переписка

        public void OnGet()
        {

        }
    }
}
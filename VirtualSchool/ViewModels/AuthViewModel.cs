using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualSchool.ViewModels
{
    public class AuthViewModel
    {
        [Required(ErrorMessage = "Вы не ввели email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Вы не ввели пароль")]
        public string Password { get; set; }
    }
}

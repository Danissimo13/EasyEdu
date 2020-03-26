using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualSchool.ViewModels
{
    public class RegViewModel
    {
        [Required(ErrorMessage = "Вы не указали email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Вы не указали имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Вы не указали фамилию")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Вы не указали пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Вы не указали номер школы")]
        public int SchoolNumber { get; set; }

        [Range(0, 11, ErrorMessage = "Класс должен быть от 1 до 11")]
        [Required(ErrorMessage = "Вы не указали номер класса")]
        public int ClassNumber { get; set; }

        [Required(ErrorMessage = "Вы не указали букву класса")]
        public string ClassChar { get; set; }
    }
}

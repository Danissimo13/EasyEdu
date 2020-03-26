using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualSchool.ViewModels
{
    public class AddDayViewModel
    {
        [Range(1,6, ErrorMessage = "Номер дня должен быть от 1 до 6")]
        [Required(ErrorMessage = "Вы не ввели номер дня недели")]
        public int DayNumber { get; set; }

        public string FirstObj { get; set; }
        public string SecondObj { get; set; }
        public string ThirdObj { get; set; }
        public string FourthObj { get; set; }
        public string FifthObj { get; set; }
        public string SixthObj { get; set; }
        public string SeventhObj { get; set; }
        public string EighthObj { get; set; }
    }
}

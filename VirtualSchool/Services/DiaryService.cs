using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualSchool.Services
{
    public class DiaryService
    {
        private int ObjNumber { get; set; }
        private Dictionary<DayOfWeek, string> DaysNames { get; set; }

        public DiaryService()
        {
            ObjNumber = 1;
            DaysNames = new Dictionary<DayOfWeek, string>();
            DaysNames.Add(DayOfWeek.Monday, "Понедельник");
            DaysNames.Add(DayOfWeek.Tuesday, "Вторник");
            DaysNames.Add(DayOfWeek.Wednesday, "Среда");
            DaysNames.Add(DayOfWeek.Thursday, "Четверг");
            DaysNames.Add(DayOfWeek.Friday, "Пятница");
            DaysNames.Add(DayOfWeek.Saturday, "Суббота");
            DaysNames.Add(DayOfWeek.Sunday, "Воскресенье");
        }

        public string this[DayOfWeek day]
        {
            get => DaysNames[day];
        }

        public int NextObj()
        {
            return ObjNumber++;
        }

        public object NewDay()
        {
            ObjNumber = 1;
            return null;
        }
    }
}

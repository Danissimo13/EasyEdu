using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualSchool.Services
{
    public class DateService
    {
        public DateTime GetMondayOfWeek()
        {
            DateTime now = DateTime.Now;
            while (now.DayOfWeek != DayOfWeek.Monday) now = now.AddDays(-1);
            return now;
        }

        public DateTime GetSaturdayOfWeek()
        {
            DateTime now = DateTime.Now;
            while (now.DayOfWeek != DayOfWeek.Saturday) now = now.AddDays(1);
            return now;
        }
    }
}

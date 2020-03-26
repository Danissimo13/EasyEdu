using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualSchool.Models
{
    public class DayObject
    {
        public int DayObjectId { get; set; }

        public Day Day { get; set; }
        public int DayId { get; set; }

        public Object Object { get; set; }
        public int ObjectId { get; set; }
    }
}

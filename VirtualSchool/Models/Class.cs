using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualSchool.Models
{
    public class Class
    {
        public int ClassId { get; set; }

        public int ClassNumber { get; set; }

        public string ClassChar { get; set; }
        
        public School School { get; set; }
        public int SchoolId { get; set; }

        public List<User> Students { get; set; }

        public List<Day> Days { get; set; }

        public Class()
        {
            Students = new List<User>();
            Days = new List<Day>();
        }
    }
}

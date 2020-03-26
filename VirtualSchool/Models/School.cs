using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualSchool.Models
{
    public class School
    {
        public int SchoolId { get; set; }

        public int SchoolNumber { get; set; }

        public List<Class> Classes { get; set; }

        public List<News> News { get; set; }

        public School()
        {
            Classes = new List<Class>();
            News = new List<News>();
        }
    }
}

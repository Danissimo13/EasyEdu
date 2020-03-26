using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualSchool.Models
{
    public class Object
    {
        public int ObjectId { get; set; }

        public string ObjectName { get; set; }

        public List<DayObject> DayObjects { get; set; }

        public Object()
        {
            DayObjects = new List<DayObject>();
        }
    }
}

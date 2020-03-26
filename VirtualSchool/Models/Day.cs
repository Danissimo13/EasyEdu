using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualSchool.Models
{
    public class Day
    {
        public int DayId { get; set; }

        public int DayNumber { get; set; }

        public DateTime Date { get; set; }

        public Class Class { get; set; }
        public int ClassId { get; set; }

        public List<DayObject> DayObjects { get; set; }

        public Day()
        {
            DayObjects = new List<DayObject>();
        }

        public void AddObject(Object obj)
        {
            DayObjects.Add(new DayObject() { Day = this, Object = obj });
        }

        public void AddChange(int objNumber, Object newObj)
        {
            DayObjects[objNumber - 1].Object = newObj; 
        }
    }
}

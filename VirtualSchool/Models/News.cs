using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualSchool.Models
{
    public class News
    {
        public int NewsId { get; set; }

        public string Head { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }

        public School School { get; set; }
        public int SchoolId { get; set; }
    }
}

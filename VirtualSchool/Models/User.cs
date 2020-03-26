using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualSchool.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public bool IsVerified { get; set; }

        public List<Message> Messages { get; set; }

        public Class Class { get; set; }
        public int ClassId { get; set; }

        public Role Role { get; set; }
        public int RoleId { get; set; }

        public User()
        {
            Messages = new List<Message>();
        }
    }
}

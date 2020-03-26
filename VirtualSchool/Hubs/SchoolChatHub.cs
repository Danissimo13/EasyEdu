using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VirtualSchool.Models;

namespace VirtualSchool.Hubs
{
    public class SchoolChatHub : Hub
    {
        private VSContext db;
        private User user;

        public SchoolChatHub(VSContext context)
        {
            db = context;
        }

        public async Task Enter()
        {
            user = db.Users.Include(u => u.Class).Where(u => (u.UserId.ToString() == Context.User.Identity.Name)).FirstOrDefault();
            await db.Schools.LoadAsync();

            string groupName = "group" + user?.Class.School.SchoolNumber.ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            string name = user.FirstName + " " + user.LastName + ":";
            await Clients.Caller.SendAsync("Enter", name, groupName);
        }

        public async Task Message(string userName,string message, string groupName)
        {
            await Clients.Group(groupName).SendAsync("Message", userName, message);
            db.Messages.Add(new Message()
            {
                AuthorId = int.Parse(Context.User.Identity.Name),
                Text = message
            });
            db.SaveChanges();
        }

    }
}

using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualSchool.Models;

namespace VirtualSchool.Hubs
{
    public class PersonalChatHub : Hub
    {
        private VSContext db;

        public PersonalChatHub(VSContext context)
        {
            db = context;
        }

        public async Task Enter()
        {
            User user = db.Users.FirstOrDefault(u => u.UserId.ToString() == Context.UserIdentifier);
            string userName = user.FirstName + " " + user.LastName;
            await Clients.Caller.SendAsync("Enter", userName, user.UserId);
        }

        public async Task Message(string toId, string message, string name)
        {
            await Clients.Caller.SendAsync("Message", message, name, Context.UserIdentifier);
            await Clients.User(toId).SendAsync("Message", message, name, Context.UserIdentifier);

            await db.PMessages.AddAsync(new PMessage()
            {
                AuthorId = int.Parse(Context.UserIdentifier),
                RecipientId = int.Parse(toId),
                Text = message
            });
            await db.SaveChangesAsync();
        }
    }
}

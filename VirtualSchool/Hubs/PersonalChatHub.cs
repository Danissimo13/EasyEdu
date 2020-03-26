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
            await Clients.Caller.SendAsync("Enter", Context.UserIdentifier);
        }

        public async Task Send(string toId, string message)
        {
            await Clients.Caller.SendAsync("Message", message);
            await Clients.User(toId).SendAsync("Message", message);

            await db.PMessages.AddAsync(new PMessage()
            {
                AuthorId = int.Parse(Context.UserIdentifier),
                RecipientId = int.Parse(toId)
            });
            await db.SaveChangesAsync();
        }
    }
}

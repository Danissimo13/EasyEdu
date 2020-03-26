using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualSchool.Models;
using VirtualSchool.ViewModels;

namespace VirtualSchool.Hubs
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "admin")]
    public class AdminHub : Hub
    {
        private VSContext db;

        public AdminHub(VSContext context)
        {
            db = context;
        }

        public async void Accept(string userId)
        {
            User user = db.Users.FirstOrDefault(u => u.UserId.ToString() == userId);

            if(user != null)
            {
                user.IsVerified = true;
                db.SaveChanges();
            }

            await Clients.Caller.SendAsync("Delete", userId);
        }

        public async void Deny(string userId)
        {
            db.Users.Remove(db.Users.FirstOrDefault(u => u.UserId.ToString() == userId));
            db.SaveChanges();

            await Clients.Caller.SendAsync("Delete", userId);
        }

        public async void AddNews(string header, string body)
        {
            User author = db.Users.Include(u => u.Class).FirstOrDefault(u => u.UserId.ToString() == Context.User.Identity.Name);
            string info = "";

            if(author != null)
            {
                db.News.Add(new News()
                {
                    Head = header,
                    Body = body,
                    Date = DateTime.Today,
                    SchoolId = author.Class.SchoolId
                });
                db.SaveChanges();
                info = "Новость успешно добавлена.";
                await Clients.Caller.SendAsync("AddNews", info);
                return;
            }

            info = "Не удалось добавить новость, попробуйте обновить страницу.";
            await Clients.Caller.SendAsync("AddNews", info);
        }
    }
}

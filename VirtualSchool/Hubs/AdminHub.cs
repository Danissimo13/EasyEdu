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
        private VSContext _db;

        public AdminHub(VSContext db)
        {
            _db = db;
        }

        public async void Accept(string userId)
        {
            User user = _db.Users.FirstOrDefault(u => u.UserId.ToString() == userId);

            if(user != null)
            {
                user.IsVerified = true;
                _db.SaveChanges();
            }

            await Clients.Caller.SendAsync("Delete", userId);
        }

        public async void Deny(string userId)
        {
            _db.Users.Remove(_db.Users.FirstOrDefault(u => u.UserId.ToString() == userId));
            _db.SaveChanges();

            await Clients.Caller.SendAsync("Delete", userId);
        }

        public async void AddNews(string header, string body)
        {
            User author = _db.Users.Include(u => u.Class).FirstOrDefault(u => u.UserId.ToString() == Context.User.Identity.Name);
            string info = "";

            if(author != null)
            {
                _db.News.Add(new News()
                {
                    Head = header,
                    Body = body,
                    Date = DateTime.Today,
                    SchoolId = author.Class.SchoolId
                });
                _db.SaveChanges();
                info = "Новость успешно добавлена.";
                await Clients.Caller.SendAsync("AddNews", info);
                return;
            }

            info = "Не удалось добавить новость, попробуйте обновить страницу.";
            await Clients.Caller.SendAsync("AddNews", info);
        }
    }
}

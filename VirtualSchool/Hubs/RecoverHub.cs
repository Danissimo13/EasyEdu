using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualSchool.Models;
using VirtualSchool.Services;

namespace VirtualSchool.Hubs
{
    public class RecoverHub : Hub
    {
        private CodeService codeService;
        private EmailService emailService;
        private VSContext db;

        public RecoverHub(VSContext context, CodeService cService, EmailService eService)
        {
            db = context;
            codeService = cService;
            emailService = eService;
        }

        public async Task SendCode(string email)
        {
            if (!db.Users.Any(u => u.Email == email))
            {
                await Clients.Caller.SendAsync("CodeSent", "");
                return;
            }

            string recoverCode = codeService.GetRandomCode();
            await emailService.SendMailAsync(email, "Ваш код восстановления - " + recoverCode, "Восстановление аккаунта");
            await Clients.Caller.SendAsync("CodeSent", recoverCode);
        }
    }
}

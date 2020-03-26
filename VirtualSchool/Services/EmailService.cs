using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace VirtualSchool.Services
{
    public class EmailService
    {
        public async Task SendMailAsync(string toEMail,string message, string subject)
        {
            MimeMessage mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Поддержка EasyEdu","kovalerov20142@yandex.com"));
            mail.To.Add(new MailboxAddress("",toEMail));
            mail.Subject = subject;
            mail.Body = new TextPart(TextFormat.Html)
            {
                Text = message
            };
            
            using(SmtpClient client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 465, true);
                await client.AuthenticateAsync("kovalerov20142@yandex.com", "danissimo");
                await client.SendAsync(mail);
                await client.DisconnectAsync(true);
            }
        }
    }
}

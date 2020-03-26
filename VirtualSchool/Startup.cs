using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VirtualSchool.Hubs;
using VirtualSchool.Models;
using VirtualSchool.Services;

namespace VirtualSchool
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionDb = "Server=DESKTOP-8TA0413;Database=VS;Trusted_Connection=True;";

            services.AddRazorPages();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => 
                {
                    options.LoginPath = new PathString("/Auth");
                    options.AccessDeniedPath = new PathString("/News");
                });
            services.AddSignalR();
            services.AddDbContext<VSContext>(options => options.UseSqlServer(connectionDb));
            services.AddTransient<IUserIdProvider, HubUserIdService>();
            services.AddTransient<DiaryService>();
            services.AddTransient<UserService>();
            services.AddTransient<CodeService>();
            services.AddSingleton<HashService>();
            services.AddSingleton<DateService>();
            services.AddSingleton<EmailService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHub<SchoolChatHub>("/schoolChatHub");
                endpoints.MapHub<PersonalChatHub>("/personalChatHub");
                endpoints.MapHub<AdminHub>("/adminhub");
                endpoints.MapHub<RecoverHub>("/recoverHub");
            });
        }
    }
}

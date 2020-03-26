using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace VirtualSchool.Models
{
    public class VSContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<PMessage> PMessages { get; set; }
        public DbSet<Object> Objects { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<DayObject> DayObjects { get; set; }

        public VSContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();

            //инициализация стартовыми данными
            if(Schools.Count() == 0)
            {
                Object math = new Object()
                {
                    ObjectName = "Математика"
                };
                Object russ = new Object()
                {
                    ObjectName = "Русский язык"
                };
                Object info = new Object()
                {
                    ObjectName = "Информатика"
                };
                Object physic = new Object()
                {
                    ObjectName = "Физика"
                };
                Object window = new Object()
                {
                    ObjectName = "Окно"
                };

                Role student = new Role()
                {
                    RoleName = "student"
                };
                Role admin = new Role()
                {
                    RoleName = "admin"
                };

                School school = new School()
                {
                    SchoolNumber = 9,
                };

                Class _class = new Class()
                {
                    ClassChar = "А",
                    ClassNumber = 10,
                    School = school,
                };

                Day one = new Day()
                {
                    Class = _class,
                    DayNumber = 6,
                    Date = DateTime.Today,
                };

                Schools.Add(school);
                Classes.Add(_class);

                Objects.Add(info);
                Objects.Add(window);
                Objects.Add(physic);
                Objects.Add(math);
                Objects.Add(russ);
                
                Days.Add(one);

                Roles.Add(student);
                Roles.Add(admin);
                SaveChanges();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>().ToTable("News");

            modelBuilder.Entity<User>().HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);
            modelBuilder.Entity<User>().HasOne(u => u.Class).WithMany(c => c.Students).HasForeignKey(u => u.ClassId);

            modelBuilder.Entity<School>().HasMany(s => s.News).WithOne(n => n.School).HasForeignKey(n => n.SchoolId);
            modelBuilder.Entity<School>().HasMany(s => s.Classes).WithOne(c => c.School).HasForeignKey(c => c.SchoolId);

            modelBuilder.Entity<Class>().HasMany(c => c.Days).WithOne(d => d.Class).HasForeignKey(d => d.ClassId);

            modelBuilder.Entity<DayObject>().HasOne(o => o.Day).WithMany(d => d.DayObjects).HasForeignKey(o => o.DayId);
            modelBuilder.Entity<DayObject>().HasOne(o => o.Object).WithMany(o => o.DayObjects).HasForeignKey(o => o.ObjectId);
        }
    }
}

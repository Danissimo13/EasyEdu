using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualSchool.Models;

namespace VirtualSchool.Services
{
    public class UserService
    {
        private VSContext db;
        private IMemoryCache cache;

        public UserService(VSContext context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
        }

        public async Task<bool> TryAddUserAsync(User user)
        {
            if(!db.Users.Any(u => u.Email == user.Email))
            {
                await db.AddAsync(user);
                await db.SaveChangesAsync();

                cache.Set(user.UserId, user, new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                });

                return true;
            }

            return false;
        }

        public async Task<User> GetUserAsync(int userId)
        {
            User profile = null;

            if(!cache.TryGetValue(userId, out profile))
            {
                profile = await db.Users.Include(u => u.Class).ThenInclude(c => c.School).FirstOrDefaultAsync(u => u.UserId == userId);
                if(profile != null)
                {
                    cache.Set(profile.UserId, profile, new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                    });
                }
            }

            return profile;
        }
    }
}

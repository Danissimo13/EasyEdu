using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSchool.Services
{
    public class HashService
    {
        private readonly string KEY;
        private HMACSHA256 SHA256;

        public HashService(IHostEnvironment environment)
        {
            string path = environment.ContentRootPath + "\\wwwroot\\key.txt";
            KEY = File.ReadAllText(path, Encoding.UTF8);
            SHA256 = new HMACSHA256(Encoding.UTF8.GetBytes(KEY));
        }

        public string GetHash(string pass)
        {
            string hash = Encoding.UTF8.GetString(SHA256.ComputeHash(Encoding.UTF8.GetBytes(pass)));

            return hash;
        }
    }
}

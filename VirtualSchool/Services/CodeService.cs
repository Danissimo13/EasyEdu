using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSchool.Services
{
    public class CodeService
    {
        private Random random;

        public CodeService()
        {
            random = new Random();
        }

        public string GetRandomCode()
        {
            StringBuilder code = new StringBuilder();

            for(int i = 0; i < 6; i++)
            {
                code.Append(random.Next(0, 9));
            }

            return code.ToString();
        }
    }
}

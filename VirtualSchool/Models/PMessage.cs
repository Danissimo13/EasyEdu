using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualSchool.Models
{
    public class PMessage
    {
        public int PMessageId { get; set; }

        public string Text { get; set; }

        public int AuthorId { get; set; }

        public int RecipientId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualSchool.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        
        public string Text { get; set; }

        public User Author { get; set; }
        public int AuthorId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MessengerNetSix.Models
{
    public class ChatList
    {
        [Required]
        public int Id { get; set; }
        public int Name { get; set; }
        [Required]
        public bool IsAGroup { get; set; }
    }
}

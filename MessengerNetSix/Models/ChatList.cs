using System.ComponentModel.DataAnnotations;

namespace MessengerNetSix.Models
{
    public class ChatList
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int ChatId { get; set; }
    }
}
